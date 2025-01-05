using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Presentation.gRPC.SDK;
using Zeus.Daemon.Application.Discord.Triggers;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Automations;
using Zeus.Daemon.Infrastructure.Integrations;
using Zeus.Daemon.Infrastructure.Services.Settings;
using Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var gRpcApiSettings = new GRpcApiSettings();

        services.Configure<IntegrationsSettings>(configuration.GetSection(IntegrationsSettings.SectionName));
        services.AddSingleton<IIntegrationsSettingsProvider, IntegrationsSettingsProvider>();
        services.AddSingleton<AutomationSynchronizationService>();
        services.AddScoped<IAutomationHandlersRegistry, AutomationHandlersRegistry>();

        services.AddTransient<DiscordMessageReceivedTriggerHandler>();

        configuration
            .GetSection(GRpcApiSettings.SectionName)
            .Bind(gRpcApiSettings);
        services.AddZeusApiGrpc(gRpcApiSettings.Address);
        return services;
    }
}
