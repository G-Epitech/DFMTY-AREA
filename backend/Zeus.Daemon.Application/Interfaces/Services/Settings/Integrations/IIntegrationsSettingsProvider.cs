namespace Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;

public interface IIntegrationsSettingsProvider
{
    public IDiscordSettingsProvider Discord { get; }
}
