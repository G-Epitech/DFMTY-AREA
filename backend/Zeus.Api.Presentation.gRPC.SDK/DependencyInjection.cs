using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Presentation.gRPC.SDK.Services;

namespace Zeus.Api.Presentation.gRPC.SDK;

public static class DependencyInjection
{
    public static void AddZeusApiGrpc(this IServiceCollection services, GrpcConfiguration configuration)
    {
        services.AddGrpcClient<Synchronization.SynchronizationClient>(o =>
        {
            
            o.Address = configuration.Host;
        });

        services.AddScoped<SynchronizationGrpcService>();
    }
}
