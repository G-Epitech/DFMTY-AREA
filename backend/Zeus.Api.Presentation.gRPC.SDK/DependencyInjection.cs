using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Presentation.gRPC.SDK.Services;

namespace Zeus.Api.Presentation.gRPC.SDK;

public static class DependencyInjection
{
    public static void AddZeusApiGrpc(this IServiceCollection services, string serverUri) {
        services.AddGrpcClient<Synchronization.SynchronizationClient>(o =>
        {
            o.Address = new Uri(serverUri);
        });

        services.AddScoped<SynchronizationGrpcService>();
    }
}
