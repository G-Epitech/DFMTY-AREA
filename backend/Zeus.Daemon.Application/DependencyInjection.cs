using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Daemon.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();
        return services;
    }
}
