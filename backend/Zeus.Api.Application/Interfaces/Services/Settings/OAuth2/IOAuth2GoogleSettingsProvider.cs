namespace Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;

public interface IOAuth2GoogleSettingsProvider
{
    public string TokenEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    public List<string> Scopes { get; }
}
