using System;
using System.Collections.Generic;
using System.Linq;
using Application.Playground;
using Db;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Application.Integration.Tests;

public class AdventurePlaygroundService_Tests : IDisposable
{
    private readonly AdventureContext _dataContext;
    private readonly IAdventurePlaygroundService _sut;

    public AdventurePlaygroundService_Tests()
    {
        var contextOptions = new DbContextOptionsBuilder<AdventureContext>()
            .UseInMemoryDatabase("AdventurePlaygroundServiceTests")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _dataContext = new AdventureContext(contextOptions);
        _sut = new AdventurePlaygroundService(_dataContext);

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
    public void GetAdventureStateReturnsImpossibleIfThereAreNoScriptSteps()
    {
        _sut.GetAdventureState().Should().Be(AdventureState.Impossible);

        _dataContext.Adventures.Add(new Adventure());
        _dataContext.SaveChanges();

        _sut.GetAdventureState().Should().Be(AdventureState.Impossible);
    }

    [Fact]
    public void GetAdventureStateReturnsNotStartedIfThereAreNoStepLogs()
    {
        _dataContext.AdventureScripts.Add(CreateScriptWithRootStep());
        _dataContext.SaveChanges();

        _sut.GetAdventureState().Should().Be(AdventureState.NotStarted);
    }

    [Fact]
    public void GetAdventureStateReturnsAdventureStateValue()
    {
        var script = CreateScriptWithRootStep();
        _dataContext.AdventureScripts.Add(script);
        _dataContext.SaveChanges();
        
        _sut.GetAdventureState().Should().Be(AdventureState.NotStarted);

        var adventure = new Adventure
        {
            AdventureScriptId = script.Id,
            Started = DateTime.UtcNow,
            AdventureStateId = (int)AdventureState.Pending
        };
        _dataContext.Adventures.Add(adventure);
        _dataContext.SaveChanges();

        _sut.GetAdventureState().Should().Be(AdventureState.Pending);
        
        adventure.AdventureStateId = (int)AdventureState.Finished;
        _dataContext.SaveChanges();
        
        _sut.GetAdventureState().Should().Be(AdventureState.Finished);
    }

    [Fact]
    public void GetCurrentStepReturnFistScriptStepIfThereIsNoActiveAdventure()
    {
        var script = CreateScriptWithRootStep();
        _dataContext.AdventureScripts.Add(script);
        _dataContext.SaveChanges();

        var step = _sut.GetCurrentStep();
        step.Text.Should().Be(script.AdventureScriptSteps.Single().Text);
    }

    [Fact]
    public void GetAdventureStateReturnsPendingIfNoAllStepsHaveLogs()
    {
        var rootStep = new AdventureScriptStep()
        {
            Text = "Root step",
            Options = new List<AdventureScriptStep>
            {
                new ()
                {
                    OptionText = "YesNoWhatever",
                    Text = "You guess"
                }
            }
        };
        var script = new AdventureScript
        {
            Created = DateTime.UtcNow,
            AdventureScriptSteps = new List<AdventureScriptStep>
            {
                rootStep
            }
        };
        _dataContext.AdventureScripts.Add(script);
        _dataContext.SaveChanges();
        
        _dataContext.Adventures.Add(new Adventure
        {
            AdventureScriptId = script.Id,
            Started = DateTime.UtcNow,
            Logs = new List<AdventureLog>
            {
                new()
                {
                    AdventureScriptStepId = rootStep.Id
                }
            }
        });
        _dataContext.SaveChanges();

        _sut.GetAdventureState().Should().Be(AdventureState.Pending);
    }

    [Fact]
    public void DeleteCurrentAdventureWorksIfThereIsNoCurrentAdventure()
    {
        _dataContext.Adventures.Any().Should().BeFalse();

        _sut.DeleteCurrentAdventure();
    }

    [Fact]
    public void DeleteCurrentAdventureWorks()
    {
        _dataContext.AdventureScripts.Add(CreateScriptWithRootStep());
        _dataContext.SaveChanges();

        _dataContext.Adventures.Add(new Adventure
        {
            AdventureScriptId = _dataContext.AdventureScripts.Single().Id,
            Started = DateTime.UtcNow,
            Logs = new List<AdventureLog>
            {
                new()
                {
                    AdventureScriptStepId = _dataContext.AdventureScriptSteps.Single().Id
                }
            }
        });

        _dataContext.SaveChanges();

        _sut.DeleteCurrentAdventure();

        _dataContext.AdventureLogs.Any().Should().BeFalse();
        _dataContext.Adventures.Any().Should().BeFalse();
    }
    
    private static AdventureScript CreateScriptWithRootStep()
    {
        return new AdventureScript
        {
            Created = DateTime.UtcNow,
            AdventureScriptSteps = new List<AdventureScriptStep>
            {
                new()
                {
                    Text = "Root step"
                }
            }
        };
    }
}