using System;
using Application.ScriptEditor;
using Db;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Application.Integration.Tests;

public class ScriptEditorService_Tests : IDisposable
{
    private readonly AdventureContext _dataContext;
    private readonly IScriptEditorService _sut;

    public ScriptEditorService_Tests()
    {
        var contextOptions = new DbContextOptionsBuilder<AdventureContext>()
            .UseInMemoryDatabase("ScriptEditorServiceTests")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _dataContext = new AdventureContext(contextOptions);
        _sut = new ScriptEditorService(_dataContext);

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
}