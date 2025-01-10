namespace Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

public interface IDiscordSettingsProvider
{
    public string ApiEndpoint { get; }
    public string OAuth2Endpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string BotToken { get; }
    public string RedirectUrl { get; }
    public List<string> Scopes { get; }
}
