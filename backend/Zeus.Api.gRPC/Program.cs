using Zeus.Api.Application;
using Zeus.Api.gRPC.Mappings;
using Zeus.Api.gRPC.Services;
using Zeus.Api.Infrastructure;
using Zeus.Common.Domain.ProvidersSettings;

var builder = WebApplication.CreateBuilder(args);
{
    await builder.Services.AddProvidersSettingsAsync();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddMappings()
        .AddGrpc(o =>
        {
            o.EnableDetailedErrors = true;
        });
}

var app = builder.Build();
{
    app.MapGrpcService<SynchronizationService>();
    app.MapGet("/",
        () =>
            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    
    app.Run();
}
