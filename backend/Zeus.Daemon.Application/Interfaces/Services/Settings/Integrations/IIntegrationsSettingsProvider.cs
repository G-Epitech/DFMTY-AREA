namespace Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;

public interface IIntegrationsSettingsProvider
{
    public IDiscordSettingsProvider Discord { get; }
    public INotionSettingsProvider Notion { get; }
    public IOpenAiSettingsProvider OpenAi { get; }
    public IRiotSettingsProvider Riot { get; }
    public IGmailSettingsProvider Gmail { get; }
}
