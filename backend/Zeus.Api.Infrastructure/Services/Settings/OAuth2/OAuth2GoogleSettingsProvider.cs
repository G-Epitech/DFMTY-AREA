using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Infrastructure.Settings.OAuth2;

namespace Zeus.Api.Infrastructure.Services.Settings.OAuth2;

public class OAuth2GoogleSettingsProvider : IOAuth2GoogleSettingsProvider
{
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    
    public OAuth2GoogleSettingsProvider(OAuth2GoogleSettings settings)
    {
        ClientId = settings.ClientId;
        ClientSecret = settings.ClientSecret;
        RedirectUrl = settings.RedirectUrl;
    }
}
