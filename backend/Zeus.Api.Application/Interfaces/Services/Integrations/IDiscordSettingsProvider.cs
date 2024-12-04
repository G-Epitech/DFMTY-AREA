namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IDiscordSettingsProvider
{
    public string ClientId { get; }
    public string RedirectUrl { get; }
    public List<string> Scope { get; }
}
