namespace Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

public interface INotionSettingsProvider
{
    public string ApiEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
}
