namespace Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

public interface IIntegrationsSettingsProvider
{
    public IDiscordSettingsProvider Discord { get; }
}
