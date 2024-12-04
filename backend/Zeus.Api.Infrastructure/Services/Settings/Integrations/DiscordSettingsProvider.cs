using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure.Services.Settings.Integrations;

public class DiscordSettingsProvider : IDiscordSettingsProvider
{
    public string ClientId { get; }
    public string RedirectUrl { get; }
    public List<string> Scope { get; }

    public DiscordSettingsProvider(DiscordSettings settings)
    {
        ClientId = settings.ClientId;
        RedirectUrl = settings.RedirectUrl;
        Scope = settings.Scope;
    }
}
