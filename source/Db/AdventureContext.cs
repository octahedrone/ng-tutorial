using Application.Playground;
using Microsoft.EntityFrameworkCore;

namespace Db;

public class AdventureContext : DbContext
{
    public AdventureContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AdventureScript> AdventureScripts { get; set; }
    public DbSet<AdventureScriptStep> AdventureScriptSteps { get; set; }
    public DbSet<Adventure> Adventures { get; set; }
    public DbSet<AdventureStateEntity> AdventureStates { get; set; }
    public DbSet<AdventureLog> AdventureLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<AdventureStateEntity>()
            .HasData(
                new AdventureStateEntity { Id = (int)AdventureState.Impossible, Title = nameof(AdventureState.Impossible) },
                new AdventureStateEntity { Id = (int)AdventureState.NotStarted, Title = nameof(AdventureState.NotStarted) },
                new AdventureStateEntity { Id = (int)AdventureState.Pending, Title = nameof(AdventureState.Pending) },
                new AdventureStateEntity { Id = (int)AdventureState.Finished, Title = nameof(AdventureState.Finished) }
            );
        
        modelBuilder.Entity<AdventureScriptStep>()
            .Property(x => x.OptionText)
            .IsRequired(false);
        
        modelBuilder.Entity<AdventureScriptStep>()
            .HasOne(x => x.AdventureScript)
            .WithMany(x => x.AdventureScriptSteps)
            .HasForeignKey(x => x.AdventureScriptId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<AdventureScriptStep>()
            .HasOne(x => x.ParentStep)
            .WithMany(x => x.Options)
            .HasForeignKey(x => x.ParentStepId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
        
        modelBuilder.Entity<Adventure>()
            .HasOne<AdventureScript>()
            .WithMany()
            .HasForeignKey(x => x.AdventureScriptId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Adventure>()
            .HasOne<AdventureStateEntity>()
            .WithMany()
            .HasForeignKey(x => x.AdventureStateId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Adventure>()
            .HasOne<AdventureScriptStep>()
            .WithMany()
            .HasForeignKey(x => x.CurrentScriptStepId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AdventureLog>()
            .HasOne(x => x.Adventure)
            .WithMany(x => x.Logs)
            .HasForeignKey(x => x.AdventureId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AdventureLog>()
            .HasOne<AdventureScriptStep>()
            .WithMany()
            .HasForeignKey(x => x.AdventureScriptStepId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}