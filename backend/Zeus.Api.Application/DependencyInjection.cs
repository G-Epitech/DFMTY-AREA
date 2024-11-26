using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
