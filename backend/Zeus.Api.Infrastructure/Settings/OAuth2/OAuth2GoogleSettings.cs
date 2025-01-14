namespace Zeus.Api.Infrastructure.Settings.OAuth2;

public class OAuth2GoogleSettings
{
    public string TokenEndpoint { get; init; } = null!;
    public string OAuth2Endpoint { get; init; } = null!;
    public OAuth2GoogleClientsSettings Clients { get; init; } = null!;
    public string ApiEndpoint { get; init; } = null!;
    public List<string> Scopes { get; init; } = null!;
}

public class OAuth2GoogleClientsSettings
{
    public OAuth2GoogleWebClientSettings Web { get; init; } = null!;
    public OAuth2GoogleAndroidClientSettings Android { get; init; } = null!;
    public OAuth2GoogleIosClientSettings Ios { get; init; } = null!;
}

public class OAuth2GoogleWebClientSettings
{
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string RedirectUrl { get; init; } = null!;
}

public class OAuth2GoogleAndroidClientSettings
{
    public string ClientId { get; init; } = null!;
}

public class OAuth2GoogleIosClientSettings
{
    public string ClientId { get; init; } = null!;
}
