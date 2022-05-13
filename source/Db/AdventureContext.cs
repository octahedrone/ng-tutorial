using Microsoft.EntityFrameworkCore;

namespace Db;

public class AdventureContext : DbContext
{
    public AdventureContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Scenario> Scenarios { get; set; }
}