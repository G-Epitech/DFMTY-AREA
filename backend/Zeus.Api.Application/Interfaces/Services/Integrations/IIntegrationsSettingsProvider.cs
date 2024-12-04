namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IIntegrationsSettingsProvider
{
    public IDiscordSettingsProvider Discord { get; }
}
