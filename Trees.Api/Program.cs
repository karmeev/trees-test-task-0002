using Autofac;
using Autofac.Extensions.DependencyInjection;
using Trees.Api.Configurations;
using Trees.Data.Migrations;
using Registry = Trees.Api.Registry;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var env = builder.Environment.EnvironmentName;

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{env}.json", false, true)
    .AddEnvironmentVariables()
    .Build();

builder.Configuration.AddConfiguration(configurationRoot);

var config = builder.Configuration.Get<ApiConfig>();
builder.Host.ConfigureContainer<ContainerBuilder>(container => Registry.RegisterDependencies(container, config));
Registry.RegisterServiceCollection(builder.Services);

var migrationContainer = new ContainerBuilder();
Trees.Data.Migrations.Registry.Register(migrationContainer, config.Infrastructure.Database.ConnectionString,
    config.Migrations);
using (var scope = migrationContainer.Build().BeginLifetimeScope())
{
    MigrationRunner.ApplyMigrations(scope);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();