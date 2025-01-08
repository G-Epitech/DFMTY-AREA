namespace Zeus.Api.Infrastructure.Settings.OAuth2;

public class OAuth2GoogleSettings
{
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string RedirectUrl { get; init; } = null!;
}
