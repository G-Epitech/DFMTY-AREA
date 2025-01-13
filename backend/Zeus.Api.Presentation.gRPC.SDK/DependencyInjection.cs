using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Presentation.gRPC.Contracts;
using Zeus.Api.Presentation.gRPC.SDK.Services;

namespace Zeus.Api.Presentation.gRPC.SDK;

public static class DependencyInjection
{
    public static void AddZeusApiGrpc(this IServiceCollection services, string serverUri)
    {
        services.AddGrpcClient<AutomationsService.AutomationsServiceClient>(o =>
        {
            o.Address = new Uri(serverUri);
        });

        services.AddScoped<AutomationService>();
    }
}
