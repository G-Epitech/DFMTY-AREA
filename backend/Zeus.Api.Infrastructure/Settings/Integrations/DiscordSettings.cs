namespace Zeus.Api.Infrastructure.Settings.Integrations;

public class DiscordSettings
{
    public string ApiEndpoint { get; init; } = null!;
    public string OAuth2Endpoint { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string BotToken { get; init; } = null!;
    public string RedirectUrl { get; init; } = null!;
    public List<string> Scope { get; init; } = null!;
}
