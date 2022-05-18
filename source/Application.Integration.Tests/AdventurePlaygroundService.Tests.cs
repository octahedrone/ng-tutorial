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
    public void HasCurrentAdventureReturnsFalseIfThereIsNoActiveAdventure()
    {
        _sut.HasCurrentAdventure().Should().BeFalse();
    }

    [Fact]
    public void HasCurrentAdventureReturnsTrueIfThereIsActiveAdventure()
    {
        _dataContext.Adventures.Add(new Adventure());
        _dataContext.SaveChanges();

        _sut.HasCurrentAdventure().Should().BeTrue();
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
}