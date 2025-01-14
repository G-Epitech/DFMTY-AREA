using Mapster;

using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Api.Integration.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddIntegrationMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(config);
        services.AddSingleton<IMapper, Mapper>();
        return services;
    }
}
