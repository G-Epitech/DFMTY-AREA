namespace Zeus.Daemon.Infrastructure.Settings.Integrations;

public class DiscordSettings
{
    public string ApiEndpoint { get; init; } = null!;
    public string WebsocketEndpoint { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string BotToken { get; init; } = null!;
}
