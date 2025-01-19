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
        OpenAi = new OpenAiSettingsProvider(settings.Value.OpenAi);
        Riot = new RiotSettingsProvider(settings.Value.Riot);
        Gmail = new GmailSettingsProvider(settings.Value.Gmail);
        Github = new GithubSettingsProvider(settings.Value.Github);
    }

    public IDiscordSettingsProvider Discord { get; }
    public INotionSettingsProvider Notion { get; }
    public IOpenAiSettingsProvider OpenAi { get; }
    public IRiotSettingsProvider Riot { get; }
    public IGithubSettingsProvider Github { get; }
    public IGmailSettingsProvider Gmail { get; }
}
