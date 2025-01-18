using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure.Services.Settings.Integrations;

public class GmailSettingsProvider : IGmailSettingsProvider
{
    public GmailSettingsProvider(GmailSettings settings)
    {
        ApiEndpoint = settings.ApiEndpoint;
        OAuth2Endpoint = settings.OAuth2Endpoint;
        OAuth2AccessType = settings.OAuth2AccessType;
        ClientId = settings.ClientId;
        ClientSecret = settings.ClientSecret;
        RedirectUrl = settings.RedirectUrl;
        Scopes = settings.Scopes;
    }

    public string ApiEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string OAuth2AccessType { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    public List<string> Scopes { get; }
}
