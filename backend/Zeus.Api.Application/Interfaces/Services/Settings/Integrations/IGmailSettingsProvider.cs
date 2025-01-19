namespace Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

public interface IGmailSettingsProvider
{
    public string ApiEndpoint { get; }
    public string TokenEndpoint { get; }
    public string UserInfoEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    public List<string> Scopes { get; }
}
