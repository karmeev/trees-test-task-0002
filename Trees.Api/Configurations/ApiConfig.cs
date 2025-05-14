using Trees.Data.Migrations.Configurations;
using Trees.Infrastructure.Configurations;

namespace Trees.Api.Configurations;

internal class ApiConfig
{
    public InfrastructureConfig Infrastructure { get; set; }
    public MigrationConfig Migrations { get; set; }
}