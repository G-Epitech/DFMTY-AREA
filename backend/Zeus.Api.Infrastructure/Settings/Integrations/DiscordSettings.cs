namespace Zeus.Api.Infrastructure.Settings.Integrations;

public class DiscordSettings
{
    public string ClientId { get; init; } = null!;
    public string RedirectUrl { get; init; } = null!;
    public List<string> Scope { get; init; } = null!;
}
