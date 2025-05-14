namespace Trees.Infrastructure.Data;

public interface IDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}