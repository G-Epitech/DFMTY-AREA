using Microsoft.Extensions.Options;

using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

public class IntegrationsSettingsProvider : IIntegrationsSettingsProvider
{
    public IntegrationsSettingsProvider(IOptions<IntegrationsSettings> settings)
    {
        Discord = new DiscordSettingsProvider(settings.Value.Discord);
        Notion = new NotionSettingsProvider(settings.Value.Notion);
        OpenAi = new OpenAiSettingsProvider(settings.Value.OpenAi);
        Riot = new RiotSettingsProvider(settings.Value.Riot);
    }

    public IDiscordSettingsProvider Discord { get; }
    public INotionSettingsProvider Notion { get; }
    public IOpenAiSettingsProvider OpenAi { get; }
    public IRiotSettingsProvider Riot { get; }
}
