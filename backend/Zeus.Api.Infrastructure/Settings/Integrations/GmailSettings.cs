namespace Zeus.Api.Infrastructure.Settings.Integrations;

public class GmailSettings
{
    public const string SectionName = nameof(GmailSettings);

    public string ApiEndpoint { get; init; } = null!;
    public string TokenEndpoint { get; init; } = null!;
    public string UserInfoEndpoint { get; init; } = null!;
    public string OAuth2Endpoint { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string RedirectUrl { get; init; } = null!;
    public List<string> Scopes { get; init; } = null!;
}
