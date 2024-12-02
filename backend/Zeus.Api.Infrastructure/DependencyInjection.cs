using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Common.Interfaces.Services;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Infrastructure.Authentication.Jwt;
using Zeus.Api.Infrastructure.Persistence.Repositories;
using Zeus.Api.Infrastructure.Services;
using Zeus.Api.Infrastructure.Settings;

namespace Zeus.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        services.AddScoped<IAuthUserContext, AuthUserContext>();

        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<UserSettings>(configuration.GetSection(UserSettings.SectionName));
        services.AddSingleton<IUserSettingsProvider, UserSettingsProvider>();

        return services;
    }

    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtAuthenticationValidation.TokenValidationParameters(jwtSettings);
                options.Events = JwtAuthenticationValidation.JwtBearerEvents();
            });

        return services;
    }
}
