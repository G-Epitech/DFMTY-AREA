namespace Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

public interface IGmailSettingsProvider
{
    public string ApiEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string OAuth2AccessType { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    public List<string> Scopes { get; }
}
