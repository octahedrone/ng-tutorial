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
    public DbSet<AdventureLog> AdventureLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<AdventureLog>()
            .HasOne<Adventure>()
            .WithMany()
            .HasForeignKey(x => x.AdventureId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AdventureLog>()
            .HasOne<AdventureScriptStep>()
            .WithMany()
            .HasForeignKey(x => x.AdventureScriptStepId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}