using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Common.Extensions.Environment;

public static class DependencyInjection
{
    public static IServiceCollection AddEnvironmentProvider(this IServiceCollection services, string environmentName)
    {
        services.AddSingleton<IEnvironmentProvider>(new EnvironmentProvider(environmentName));
        return services;
    }
}
