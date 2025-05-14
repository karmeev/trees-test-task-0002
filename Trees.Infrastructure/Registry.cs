using Autofac;
using Microsoft.EntityFrameworkCore;
using Trees.Infrastructure.Configurations;
using Trees.Infrastructure.Data;
using Trees.Infrastructure.Database;

namespace Trees.Infrastructure;

public static class Registry
{
    public static void RegisterDependencies(ContainerBuilder container, InfrastructureConfig config)
    {
        container.RegisterInstance(config).SingleInstance();
        container.Register(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TreesDbContext>();
                var infrastructureConfig = context.Resolve<InfrastructureConfig>();
                optionsBuilder.UseNpgsql(infrastructureConfig.Database.ConnectionString);
                return new TreesDbContext(optionsBuilder.Options);
            })
            .As<IDbContext>()
            .InstancePerLifetimeScope();
    }
}