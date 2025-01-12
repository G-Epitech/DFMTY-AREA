using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Integration.Mapping;
using Zeus.Api.Presentation.gRPC.SDK;
using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Application.Discord.TriggerHandlers;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Automations;
using Zeus.Daemon.Infrastructure.Integrations;
using Zeus.Daemon.Infrastructure.Integrations.Api;
using Zeus.Daemon.Infrastructure.Services.Discord;
using Zeus.Daemon.Infrastructure.Services.Settings;
using Zeus.Daemon.Infrastructure.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        var gRpcApiSettings = new GRpcApiSettings();

        services.Configure<IntegrationsSettings>(configuration.GetSection(IntegrationsSettings.SectionName));
        services.AddSingleton<IIntegrationsSettingsProvider, IntegrationsSettingsProvider>();
        services.AddSingleton<AutomationSynchronizationService>();

        services.AddTransient<DiscordMessageReceivedTriggerHandler>();

        services.AddSingleton<IDiscordWebSocketService, DiscordWebSocketService>();
        services.AddSingleton<IDiscordApiService, DiscordApiService>();

        configuration
            .GetSection(GRpcApiSettings.SectionName)
            .Bind(gRpcApiSettings);
        services.AddZeusApiGrpc(gRpcApiSettings.Address);
        services.AddMessageBroker(configuration, typeof(DependencyInjection).Assembly);
        services.AddSingleton<BusHandler>();
        services.AddIntegrationMappings();
        return services;
    }
}
