using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Common.Interfaces.Services;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Infrastructure.Authentication;
using Zeus.Api.Infrastructure.Persistence.Repositories;
using Zeus.Api.Infrastructure.Services;

namespace Zeus.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.Configure<JwtSettings>(builderConfiguration.GetSection(JwtSettings.SectionName));

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        
        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
