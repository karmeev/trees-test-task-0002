using Autofac;
using Microsoft.EntityFrameworkCore;
using Trees.Infrastructure.Database;

namespace Trees.Data.Migrations;

public static class MigrationRunner
{
    public static void ApplyMigrations(ILifetimeScope lifetimeScope)
    {
        var dbContext = lifetimeScope.Resolve<TreesDbContext>();
        dbContext.Database.Migrate();
    }
}