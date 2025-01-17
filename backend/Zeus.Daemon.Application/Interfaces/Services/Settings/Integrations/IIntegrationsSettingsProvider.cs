namespace Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;

public interface IIntegrationsSettingsProvider
{
    public IDiscordSettingsProvider Discord { get; }
    public INotionSettingsProvider Notion { get; }
    public IRiotSettingsProvider Riot { get; }
}
