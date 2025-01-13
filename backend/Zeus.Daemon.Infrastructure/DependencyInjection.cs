using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Integration.Mapping;
using Zeus.Api.Presentation.gRPC.SDK;
using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Application.Interfaces;
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
        services.Configure<IntegrationsSettings>(configuration.GetSection(IntegrationsSettings.SectionName));
        services.AddSingleton<IIntegrationsSettingsProvider, IntegrationsSettingsProvider>();

        services.AddSingleton<IDaemonService, AutomationSynchronizationService>();

        #region Discord

        services.AddService<DiscordWebSocketService, IDiscordWebSocketService, IDaemonService>(ServiceLifetime.Singleton);
        services.AddSingleton<IDiscordApiService, DiscordApiService>();

        #endregion

        #region GRPC

        var gRpcApiSettings = new GRpcApiSettings();

        configuration
            .GetSection(GRpcApiSettings.SectionName)
            .Bind(gRpcApiSettings);
        services.AddZeusApiGrpc(gRpcApiSettings.Address);

        #endregion

        #region MessageBroker

        services.AddMessageBroker(configuration, typeof(DependencyInjection).Assembly);
        services.AddSingleton<BusHandler>();

        #endregion

        services.AddIntegrationMappings();

        return services;
    }
}
