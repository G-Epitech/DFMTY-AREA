using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Application.Interfaces.Services.Settings;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.AutomationAggregate.Enums;
using Zeus.Api.Domain.Integrations.Common.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Infrastructure.Authentication.Jwt;
using Zeus.Api.Infrastructure.Persistence;
using Zeus.Api.Infrastructure.Persistence.Interceptors;
using Zeus.Api.Infrastructure.Persistence.Repositories;
using Zeus.Api.Infrastructure.Services;
using Zeus.Api.Infrastructure.Services.Integrations.Discord;
using Zeus.Api.Infrastructure.Services.Settings;
using Zeus.Api.Infrastructure.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        services.AddScoped<IIntegrationReadRepository, IntegrationReadRepository>();
        services.AddScoped<IIntegrationWriteRepository, IntegrationWriteRepository>();
        services.AddScoped<IIntegrationLinkRequestReadRepository, IntegrationLinkRequestReadRepository>();
        services.AddScoped<IIntegrationLinkRequestWriteRepository, IntegrationLinkRequestWriteRepository>();
        services.AddScoped<IAutomationReadRepository, AutomationReadRepository>();
        services.AddScoped<IAutomationWriteRepository, AutomationWriteRepository>();

        services.AddScoped<IAuthUserContext, AuthUserContext>();

        services.AddScoped<IDiscordService, DiscordService>();

        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddDatabase(configuration);
        services.AddConfiguration(configuration);

        return services;
    }

    private static void AddConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<UserSettings>(configuration.GetSection(UserSettings.SectionName));
        services.AddSingleton<IUserSettingsProvider, UserSettingsProvider>();

        services.Configure<IntegrationsSettings>(configuration.GetSection(IntegrationsSettings.SectionName));
        services.AddSingleton<IIntegrationsSettingsProvider, IntegrationsSettingsProvider>();

        services.Configure<ServicesSettings>(configuration.GetSection(ServicesSettings.SectionName));
        services.AddSingleton<IServicesSettingsProvider, ServicesSettingsProvider>();
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    JwtAuthenticationValidation.GetTokenValidationParameters(jwtSettings);
                options.Events = JwtAuthenticationValidation.GetJwtBearerEvents();
            });
        return services;
    }

    private static void AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStrings.DefaultDatabase);

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        services.AddDbContext<ZeusDbContext>(options =>
        {
            options.UseNpgsql(connectionString, o =>
            {
                o.MapEnum<IntegrationType>(nameof(IntegrationType));
                o.MapEnum<IntegrationTokenUsage>(nameof(IntegrationTokenUsage));
                o.MapEnum<AutomationActionParameterType>(nameof(AutomationActionParameterType));
            });
        });

        services.AddScoped<AuditableEntitiesInterceptor>();
    }
}
