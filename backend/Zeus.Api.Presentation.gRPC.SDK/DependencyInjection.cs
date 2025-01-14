using Grpc.Net.ClientFactory;

using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Presentation.gRPC.Contracts;
using Zeus.Api.Presentation.gRPC.SDK.Services;

namespace Zeus.Api.Presentation.gRPC.SDK;

using AutomationsServiceClient = AutomationsService.AutomationsServiceClient;
using IntegrationsServiceClient = IntegrationsService.IntegrationsServiceClient;

public static class DependencyInjection
{
    public static void AddZeusApiGrpc(this IServiceCollection services, string serverUri)
    {
        var configuration = GetConfiguration(serverUri);

        services.AddGrpcClient<AutomationsServiceClient>(configuration);
        services.AddGrpcClient<IntegrationsServiceClient>(configuration);

        services.AddScoped<IIntegrationsService, Services.Implementations.IntegrationsService>();
        services.AddScoped<IAutomationsService, Services.Implementations.AutomationsService>();
    }

    private static Action<GrpcClientFactoryOptions> GetConfiguration(string serverUri) => o => { o.Address = new Uri(serverUri); };
}
