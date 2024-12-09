namespace Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;

public interface IDiscordSettingsProvider
{
    public string ApiEndpoint { get; }
    public string WebsocketEndpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
}
