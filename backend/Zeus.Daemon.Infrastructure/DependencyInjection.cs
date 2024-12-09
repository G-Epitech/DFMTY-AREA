using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Integrations;
using Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IntegrationsSettings>(configuration.GetSection(IntegrationsSettings.SectionName));
        services.AddSingleton<IIntegrationsSettingsProvider, IntegrationsSettingsProvider>();

        return services;
    }
}
