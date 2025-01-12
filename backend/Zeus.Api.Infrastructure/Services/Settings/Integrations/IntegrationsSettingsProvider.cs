using Microsoft.Extensions.Options;

using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure.Services.Settings.Integrations;

public class IntegrationsSettingsProvider : IIntegrationsSettingsProvider
{
    public IntegrationsSettingsProvider(IOptions<IntegrationsSettings> settings)
    {
        Discord = new DiscordSettingsProvider(settings.Value.Discord);
        Notion = new NotionSettingsProvider(settings.Value.Notion);
    }

    public IDiscordSettingsProvider Discord { get; }
    public INotionSettingsProvider Notion { get; }
}
