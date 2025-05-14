using Autofac;
using Trees.Api.Configurations;

namespace Trees.Api;

internal static class Registry
{
    public static void RegisterServiceCollection(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void RegisterDependencies(ContainerBuilder container, ApiConfig config)
    {
        Infrastructure.Registry.RegisterDependencies(container, config.Infrastructure);
    }
}