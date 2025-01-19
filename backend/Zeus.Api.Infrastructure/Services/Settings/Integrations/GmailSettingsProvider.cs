using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure.Services.Settings.Integrations;

public class GmailSettingsProvider : IGmailSettingsProvider
{
    public GmailSettingsProvider(GmailSettings settings)
    {
        ApiEndpoint = settings.ApiEndpoint;
        TokenEndpoint = settings.TokenEndpoint;
        UserInfoEndpoint = settings.UserInfoEndpoint;
        OAuth2Endpoint = settings.OAuth2Endpoint;
        ClientId = settings.ClientId;
        ClientSecret = settings.ClientSecret;
        RedirectUrl = settings.RedirectUrl;
        Scopes = settings.Scopes;
    }

    public string ApiEndpoint { get; }
    public string TokenEndpoint { get; }
    public string UserInfoEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    public List<string> Scopes { get; }
}
