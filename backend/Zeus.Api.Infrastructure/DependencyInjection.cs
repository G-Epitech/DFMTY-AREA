using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Application.Interfaces.Services.Integrations.Notion;
using Zeus.Api.Application.Interfaces.Services.OAuth2;
using Zeus.Api.Application.Interfaces.Services.Settings;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Infrastructure.Authentication.Jwt;
using Zeus.Api.Infrastructure.Persistence;
using Zeus.Api.Infrastructure.Persistence.Interceptors;
using Zeus.Api.Infrastructure.Persistence.Repositories;
using Zeus.Api.Infrastructure.Services;
using Zeus.Api.Infrastructure.Services.Integrations.Discord;
using Zeus.Api.Infrastructure.Services.Integrations.Notion;
using Zeus.Api.Infrastructure.Services.OAuth2.Google;
using Zeus.Api.Infrastructure.Services.Settings;
using Zeus.Api.Infrastructure.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Services.Settings.OAuth2;
using Zeus.Api.Infrastructure.Settings;
using Zeus.Api.Infrastructure.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.OAuth2;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;

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
        services.AddScoped<IAuthenticationMethodReadRepository, AuthenticationMethodReadRepository>();
        services.AddScoped<IAuthenticationMethodWriteRepository, AuthenticationMethodWriteRepository>();

        services.AddScoped<IAuthUserContext, AuthUserContext>();

        services.AddScoped<IDiscordService, DiscordService>();
        services.AddScoped<INotionService, NotionService>();
        services.AddScoped<IGoogleOAuth2Service, GoogleOAuth2Service>();

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

        services.Configure<OAuth2Settings>(configuration.GetSection(OAuth2Settings.SectionName));
        services.AddSingleton<IOAuth2SettingsProvider, OAuth2SettingsProvider>();
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
                o.MapEnum<AuthenticationMethodType>(nameof(AuthenticationMethodType));
            });
        });

        services.AddScoped<AuditableEntitiesInterceptor>();
    }
}
