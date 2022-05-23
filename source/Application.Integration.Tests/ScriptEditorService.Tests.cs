using System;
using System.Collections.Generic;
using System.Linq;
using Application.Playground;
using Application.ScriptEditor;
using Db;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NSubstitute;
using Xunit;

namespace Application.Integration.Tests;

public class ScriptEditorService_Tests : IDisposable
{
    private readonly AdventureContext _dataContext;
    private readonly IAdventurePlaygroundService _playgroundServiceMock;
    private readonly IScriptEditorService _sut;

    public ScriptEditorService_Tests()
    {
        var contextOptions = new DbContextOptionsBuilder<AdventureContext>()
            .UseInMemoryDatabase("ScriptEditorServiceTests")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _playgroundServiceMock = Substitute.For<IAdventurePlaygroundService>();
        _dataContext = new AdventureContext(contextOptions);
        _sut = new ScriptEditorService(_dataContext, _playgroundServiceMock);

        _dataContext.Database.EnsureDeleted();
        _dataContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        if (_dataContext != null)
        {
            _dataContext.Dispose();
        }
    }

    [Fact]
    public void HasCurrentScriptReturnsFalseIfThereIsNoActiveScript()
    {
        _sut.HasCurrentScript().Should().BeFalse();
    }

    [Fact]
    public void HasCurrentScriptReturnsTrueIfThereIsActiveScript()
    {
        _dataContext.AdventureScripts.Add(new AdventureScript
        {
            Created = DateTime.UtcNow
        });
        _dataContext.SaveChanges();

        _sut.HasCurrentScript().Should().BeTrue();
    }

    [Fact]
    public void GetCurrentScriptReturnsNullIfThereIsNoScript()
    {
        _sut.GetCurrentScript().Should().BeNull();
    }

    [Fact]
    public void GetCurrentScriptReturnsScriptWithSteps()
    {
        var scriptEntity = new AdventureScript
        {
            Created = DateTime.UtcNow
        };
        _dataContext.AdventureScripts.Add(scriptEntity);

        var option2 = new AdventureScriptStep()
        {
            AdventureScript = scriptEntity,
            OptionText = "Option 2",
            Text = "Option 2 text",
            OrderNumber = 2
        };
        var option1 = new AdventureScriptStep()
        {
            AdventureScript = scriptEntity,
            OptionText = "Option 1",
            Text = "Option 1 text",
            OrderNumber = 1
        };
        var root = new AdventureScriptStep()
        {
            Text = "Root step",
            Options = new List<AdventureScriptStep>
            {
                option2,
                option1
            }
        };
        scriptEntity.AdventureScriptSteps = new List<AdventureScriptStep>
        {
            root
        };

        _dataContext.SaveChanges();

        var script = _sut.GetCurrentScript();
        script.Root.Should().NotBeNull();
        script.Root.Text.Should().Be(root.Text);
        script.Root.Options.Should().NotBeNull();
        script.Root.Options.Count.Should().Be(2);
        script.Root.Options.Should()
            .Contain(x => x.OptionText == option1.OptionText
                && x.Text == option1.Text
                && x.OrderNumber == option1.OrderNumber
            );
        script.Root.Options.Should()
            .Contain(x => x.OptionText == option2.OptionText
                && x.Text == option2.Text
                && x.OrderNumber == option2.OrderNumber
            );
    }

    [Fact]
    public void TestDeleteCurrentScriptWithAdventure()
    {
        _dataContext.AdventureScripts.Add(new AdventureScript
        {
            Created = DateTime.UtcNow,
            AdventureScriptSteps = new List<AdventureScriptStep>
            {
                new()
                {
                    Text = "Root step"
                }
            }
        });
        _dataContext.SaveChanges();

        _sut.DeleteCurrentScript();

        _dataContext.AdventureScriptSteps.Any().Should().BeFalse();
        _dataContext.AdventureScripts.Any().Should().BeFalse();
        _playgroundServiceMock
            .Received(1)
            .DeleteCurrentAdventure();
    }

    [Fact]
    public void ReplaceCurrentScriptWorksFineWithNoExistingScript()
    {
        var option1 = new ScriptEditorScriptStep()
        {
            OptionText = "Option 1",
            Text = "Option 1 text",
        };
        var option21 = new ScriptEditorScriptStep()
        {
            OptionText = "Option 2 1",
            Text = "Option 2 1 text"
        };
        var option2 = new ScriptEditorScriptStep()
        {
            OptionText = "Option 2",
            Text = "Option 2 text",
            Options = new List<ScriptEditorScriptStep>
            {
                option21
            }
        };
        var rootStep = new ScriptEditorScriptStep
        {
            Text = "Root step",
            Options = new List<ScriptEditorScriptStep>
            {
                option1,
                option2,
            }
        };
        var editorScript = new ScriptEditorScript
        {
            Created = DateTime.UtcNow,
            Root = rootStep
        };

        _dataContext.AdventureScriptSteps.Any().Should().BeFalse();
        _dataContext.AdventureScripts.Any().Should().BeFalse();
        
        _sut.ReplaceCurrentScript(editorScript);

        var scriptEntity = _dataContext.AdventureScripts
            .Single();

        var scriptSteps = _dataContext.AdventureScriptSteps
            .Where(x => x.AdventureScriptId == scriptEntity.Id)
            .Include(x => x.AdventureScript)
            .Include(x => x.ParentStep)
            .ToList();
        
        scriptSteps.Count.Should().Be(4);

        // only one root step
        scriptSteps.Count(x => x.ParentStepId == null).Should().Be(1);
        
        // root step
        scriptSteps.Should()
            .Contain(x =>
                x.ParentStepId == null
                && x.Text == rootStep.Text
                && x.OptionText == rootStep.OptionText);

        var stepsWithParents = scriptSteps.Where(x => x.ParentStep != null);
        
        // option1
        stepsWithParents.Should()
            .Contain(x =>
                x.ParentStep.Text == rootStep.Text
                && x.Text == option1.Text
                && x.OptionText == option1.OptionText);
        
        // option2
        stepsWithParents.Should()
            .Contain(x =>
                x.ParentStep.Text == rootStep.Text
                && x.Text == option2.Text
                && x.OptionText == option2.OptionText);
        
        // option21
        stepsWithParents.Should()
            .Contain(x =>
                x.ParentStep.Text == option2.Text
                && x.Text == option21.Text
                && x.OptionText == option21.OptionText);
    }
}