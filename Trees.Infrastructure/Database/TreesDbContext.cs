using Microsoft.EntityFrameworkCore;
using Trees.Infrastructure.Data;

namespace Trees.Infrastructure.Database;

internal class TreesDbContext(DbContextOptions<TreesDbContext> options) : DbContext(options), IDbContext
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Custom message: Saving to database failed.", ex);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:MaxIdentifierLength", 63);
        base.OnModelCreating(modelBuilder);
    }
}