using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.gRPC.SDK;
using Zeus.Daemon.Application.Discord.Triggers;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Automations;
using Zeus.Daemon.Infrastructure.Integrations;
using Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IntegrationsSettings>(configuration.GetSection(IntegrationsSettings.SectionName));
        services.AddSingleton<IIntegrationsSettingsProvider, IntegrationsSettingsProvider>();
        services.AddSingleton<AutomationSynchronizationService>();
        services.AddScoped<IAutomationHandlersRegistry, AutomationHandlersRegistry>();
        
        services.AddTransient<DiscordMessageReceivedTriggerHandler>();
        
        services.AddZeusApiGrpc(new GrpcConfiguration
        {
            Host = new Uri("http://localhost:5069")
        });
        return services;
    }
}
