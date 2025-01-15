namespace Zeus.Api.Infrastructure.Settings.Integrations;

public class RiotSettings
{
    public string PlatformApiEndpoint { get; init; } = null!;
    public string RegionalApiEndpoint { get; init; } = null!;
    public string DataDragonApiEndpoint { get; init; } = null!;
    public string ApiKey { get; init; } = null!;
}
