namespace Zeus.Api.Infrastructure.Settings.OAuth2;

public class OAuth2GoogleSettings
{
    public string TokenEndpoint { get; init; } = null!;
    public string OAuth2Endpoint { get; init; } = null!;
    public string ApiEndpoint { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string RedirectUrl { get; init; } = null!;
    public List<string> Scopes { get; init; } = null!;
}
