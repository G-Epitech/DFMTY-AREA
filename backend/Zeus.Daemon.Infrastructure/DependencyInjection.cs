using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Integration.Mapping;
using Zeus.Api.Presentation.gRPC.SDK;
using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Providers.Discord.Services;
using Zeus.Daemon.Application.Providers.Github.Services;
using Zeus.Daemon.Application.Providers.LeagueOfLegends.Services;
using Zeus.Daemon.Application.Providers.Notion.Services;
using Zeus.Daemon.Application.Providers.OpenAi.Services;
using Zeus.Daemon.Infrastructure.Integrations;
using Zeus.Daemon.Infrastructure.Integrations.Api;
using Zeus.Daemon.Infrastructure.Services.Api;
using Zeus.Daemon.Infrastructure.Services.Providers.Discord;
using Zeus.Daemon.Infrastructure.Services.Providers.Github;
using Zeus.Daemon.Infrastructure.Services.Providers.LeagueOfLegends;
using Zeus.Daemon.Infrastructure.Services.Providers.Notion;
using Zeus.Daemon.Infrastructure.Services.Providers.OpenAi;
using Zeus.Daemon.Infrastructure.Services.Settings;
using Zeus.Daemon.Infrastructure.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.Configure<IntegrationsSettings>(configuration.GetSection(IntegrationsSettings.SectionName));
        services.AddSingleton<IIntegrationsSettingsProvider, IntegrationsSettingsProvider>();

        services.AddSingleton<IDaemonService, SynchronizationService>();
        services.AddSingleton<IIntegrationsProvider, IntegrationsProvider>();

        #region Discord

        services.AddService<DiscordWebSocketService, IDiscordWebSocketService, IDaemonService>(
            ServiceLifetime.Singleton);
        services.AddSingleton<IDiscordApiService, DiscordApiService>();

        #endregion

        #region Notion

        services.AddService<NotionPollingService, INotionPollingService, IDaemonService>(
            ServiceLifetime.Singleton);
        services.AddSingleton<INotionApiService, NotionApiService>();

        #endregion

        #region OpenAi

        services.AddSingleton<IOpenAiApiService, OpenAiApiService>();

        #endregion OpenAi

        #region LeagueOfLegends

        services.AddService<LeagueOfLegendsPollingService, ILeagueOfLegendsPollingService, IDaemonService>(
            ServiceLifetime.Singleton);
        services.AddSingleton<ILeagueOfLegendsApiService, LeagueOfLegendsApiService>();

        #endregion
        
        #region Github
        
        services.AddSingleton<IGithubApiService, GithubApiService>();
        services.AddService<GithubPollingService, IGithubPollingService, IDaemonService>(
            ServiceLifetime.Singleton);
        
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
