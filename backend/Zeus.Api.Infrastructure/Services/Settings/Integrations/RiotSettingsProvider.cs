using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure.Services.Settings.Integrations;

public class RiotSettingsProvider : IRiotSettingsProvider
{
    public string PlatformApiEndpoint { get; }
    public string RegionalApiEndpoint { get; }
    public string DataDragonApiEndpoint { get; }
    public string ApiKey { get; }

    public RiotSettingsProvider(RiotSettings settings)
    {
        PlatformApiEndpoint = settings.PlatformApiEndpoint;
        RegionalApiEndpoint = settings.RegionalApiEndpoint;
        DataDragonApiEndpoint = settings.DataDragonApiEndpoint;
        ApiKey = settings.ApiKey;
    }
}
