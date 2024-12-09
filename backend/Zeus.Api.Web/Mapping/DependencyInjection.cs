using System.Reflection;

using Mapster;

using MapsterMapper;

using Zeus.Api.Application.Integrations.Query.GetIntegration;

namespace Zeus.Api.Web.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        config.Scan(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}
