using Microsoft.Extensions.Options;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Integrations;

namespace Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

public class IntegrationsSettingsProvider : IIntegrationsSettingsProvider
{
    public IDiscordSettingsProvider Discord { get; }
    public INotionSettingsProvider Notion { get; }

    public IntegrationsSettingsProvider(IOptions<IntegrationsSettings> settings)
    {
        Discord = new DiscordSettingsProvider(settings.Value.Discord);
        Notion = new NotionSettingsProvider(settings.Value.Notion);
    }
}
