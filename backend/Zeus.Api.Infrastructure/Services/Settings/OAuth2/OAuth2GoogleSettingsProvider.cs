using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Infrastructure.Settings.OAuth2;

namespace Zeus.Api.Infrastructure.Services.Settings.OAuth2;

public class OAuth2GoogleSettingsProvider : IOAuth2GoogleSettingsProvider
{
    public OAuth2GoogleSettingsProvider(OAuth2GoogleSettings settings)
    {
        TokenEndpoint = settings.TokenEndpoint;
        OAuth2Endpoint = settings.OAuth2Endpoint;
        Clients = new OAuth2GoogleClientsSettingsProvider(settings.Clients);
        ApiEndpoint = settings.ApiEndpoint;
        Scopes = settings.Scopes;
    }

    public string TokenEndpoint { get; }
    public string OAuth2Endpoint { get; }

    public IOAuth2GoogleClientsSettingsProvider Clients { get; }
    public string ApiEndpoint { get; }
    public List<string> Scopes { get; }
}

public class OAuth2GoogleClientsSettingsProvider : IOAuth2GoogleClientsSettingsProvider
{
    public OAuth2GoogleClientsSettingsProvider(OAuth2GoogleClientsSettings settings)
    {
        Web = new OAuth2GoogleWebClientSettingsProvider(settings.Web);
        Android = new OAuth2GoogleAndroidClientSettingsProvider(settings.Android);
        Ios = new OAuth2GoogleIosClientSettingsProvider(settings.Ios);
    }

    public IOAuth2GoogleWebClientSettingsProvider Web { get; }
    public IOAuth2GoogleAndroidClientSettingsProvider Android { get; }
    public IOAuth2GoogleIosClientSettingsProvider Ios { get; }
}

public class OAuth2GoogleWebClientSettingsProvider : IOAuth2GoogleWebClientSettingsProvider
{
    public OAuth2GoogleWebClientSettingsProvider(OAuth2GoogleWebClientSettings settings)
    {
        ClientId = settings.ClientId;
        ClientSecret = settings.ClientSecret;
        RedirectUrl = settings.RedirectUrl;
    }

    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
}

public class OAuth2GoogleAndroidClientSettingsProvider : IOAuth2GoogleAndroidClientSettingsProvider
{
    public OAuth2GoogleAndroidClientSettingsProvider(OAuth2GoogleAndroidClientSettings settings)
    {
        ClientId = settings.ClientId;
    }

    public string ClientId { get; }
}

public class OAuth2GoogleIosClientSettingsProvider : IOAuth2GoogleIosClientSettingsProvider
{
    public OAuth2GoogleIosClientSettingsProvider(OAuth2GoogleIosClientSettings settings)
    {
        ClientId = settings.ClientId;
    }

    public string ClientId { get; }
}
