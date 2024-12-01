using Duster.Models;
using Microsoft.EntityFrameworkCore;

namespace Duster;

public class ExecutionDbContext(DbContextOptions<ExecutionDbContext> options) : DbContext(options)
{
    public DbSet<Execution> Executions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Execution>()
            .Property(e => e.Timestamp)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}