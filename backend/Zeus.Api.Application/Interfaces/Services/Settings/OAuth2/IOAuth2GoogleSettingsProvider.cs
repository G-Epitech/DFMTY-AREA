namespace Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;

public interface IOAuth2GoogleSettingsProvider
{
    public string TokenEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public IOAuth2GoogleClientsSettingsProvider Clients { get; }
    public string ApiEndpoint { get; }
    public List<string> Scopes { get; }
}

public interface IOAuth2GoogleClientsSettingsProvider
{
    public IOAuth2GoogleWebClientSettingsProvider Web { get; }
    public IOAuth2GoogleAndroidClientSettingsProvider Android { get; }
    public IOAuth2GoogleIosClientSettingsProvider Ios { get; }
}

public interface IOAuth2GoogleClientSettingsProvider
{
    public string ClientId { get; }
}

public interface IOAuth2GoogleWebClientSettingsProvider : IOAuth2GoogleClientSettingsProvider
{
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
}

public interface IOAuth2GoogleAndroidClientSettingsProvider : IOAuth2GoogleClientSettingsProvider
{}

public interface IOAuth2GoogleIosClientSettingsProvider : IOAuth2GoogleClientSettingsProvider
{}
