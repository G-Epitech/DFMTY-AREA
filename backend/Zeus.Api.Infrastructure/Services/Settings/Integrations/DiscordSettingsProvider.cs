using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure.Services.Settings.Integrations;

public class DiscordSettingsProvider : IDiscordSettingsProvider
{
    public string ApiEndpoint { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl { get; }
    public List<string> Scope { get; }

    public DiscordSettingsProvider(DiscordSettings settings)
    {
        ApiEndpoint = settings.ApiEndpoint;
        ClientId = settings.ClientId;
        ClientSecret = settings.ClientSecret;
        RedirectUrl = settings.RedirectUrl;
        Scope = settings.Scope;
    }
}
