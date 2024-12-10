using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Api.gRPC.SDK;

public static class DependencyInjection
{
    public static void AddZeusApiGrpc(this IServiceCollection services)
    {
        services.AddGrpcClient<Synchronization.SynchronizationClient>(o =>
        {
            o.Address = new Uri("http://localhost:5069");
        });

        services.AddScoped<SynchronizationGrpcService>();
    }
}
