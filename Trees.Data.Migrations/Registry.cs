using Autofac;
using Microsoft.EntityFrameworkCore;
using Trees.Data.Migrations.Configurations;
using Trees.Infrastructure.Data;
using Trees.Infrastructure.Database;

namespace Trees.Data.Migrations;

public static class Registry
{
    public static void Register(ContainerBuilder container, string connection, MigrationConfig config)
    {
        config.ConnectionString = connection;
        container.RegisterInstance(config).SingleInstance();
        container.Register(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TreesDbContext>();
                var migrationConfig = context.Resolve<MigrationConfig>();
                optionsBuilder.UseNpgsql(
                    connectionString: migrationConfig.ConnectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsHistoryTable(migrationConfig.TableName);
                        npgsqlOptions.MigrationsAssembly(typeof(TreesDbContext).Assembly.FullName);
                    });
                return new TreesDbContext(optionsBuilder.Options);
            })
            .As<TreesDbContext>()
            .As<IDbContext>()
            .InstancePerLifetimeScope();
    }
}