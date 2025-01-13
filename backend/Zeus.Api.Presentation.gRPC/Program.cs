using Zeus.Api.Application;
using Zeus.Api.Infrastructure;
using Zeus.Api.Presentation.gRPC.Mappings;
using Zeus.Api.Presentation.gRPC.Services;
using Zeus.Api.Presentation.Shared;
using Zeus.Common.Domain.ProvidersSettings;

var builder = WebApplication.CreateBuilder(args);
{
    #region Configuration

    builder.Configuration
        .AddSharedAppSettings()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

    #endregion Configuration

    #region Services

    await builder.Services.AddProvidersSettingsAsync();
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddMappings()
        .AddGrpc(o =>
        {
            o.EnableDetailedErrors = true;
        });

    #endregion Services
}

var app = builder.Build();
{
    app.MapGrpcService<AutomationsService>();
    app.MapGet("/",
        () =>
            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

    app.Run();
}
