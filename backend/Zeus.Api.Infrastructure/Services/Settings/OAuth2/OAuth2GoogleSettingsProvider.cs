using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Infrastructure.Settings.OAuth2;

namespace Zeus.Api.Infrastructure.Services.Settings.OAuth2;

public class OAuth2GoogleSettingsProvider : IOAuth2GoogleSettingsProvider
{
    public string TokenEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string ApiEndpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    public List<string> Scopes { get; }
    
    public OAuth2GoogleSettingsProvider(OAuth2GoogleSettings settings)
    {
        TokenEndpoint = settings.TokenEndpoint;
        OAuth2Endpoint = settings.OAuth2Endpoint;
        ApiEndpoint = settings.ApiEndpoint;
        ClientId = settings.ClientId;
        ClientSecret = settings.ClientSecret;
        RedirectUrl = settings.RedirectUrl;
        Scopes = settings.Scopes;
    }
}
