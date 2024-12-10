using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Integrations;

namespace Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

public class DiscordSettingsProvider : IDiscordSettingsProvider
{
    public string ApiEndpoint { get; }
    public string WebsocketEndpoint { get; }
    public string ClientId { get; }
    public string BotToken { get; }

    public DiscordSettingsProvider(DiscordSettings settings)
    {
        ApiEndpoint = settings.ApiEndpoint;
        WebsocketEndpoint = settings.WebsocketEndpoint;
        ClientId = settings.ClientId;
        BotToken = settings.BotToken;
    }
}
