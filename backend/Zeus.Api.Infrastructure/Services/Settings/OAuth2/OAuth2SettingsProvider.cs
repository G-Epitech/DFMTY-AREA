using Microsoft.Extensions.Options;

using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Infrastructure.Settings.OAuth2;

namespace Zeus.Api.Infrastructure.Services.Settings.OAuth2;

public class OAuth2SettingsProvider : IOAuth2SettingsProvider
{
    public OAuth2SettingsProvider(IOptions<OAuth2Settings> settings)
    {
        Google = new OAuth2GoogleSettingsProvider(settings.Value.Google);
    }

    public IOAuth2GoogleSettingsProvider Google { get; }
}
