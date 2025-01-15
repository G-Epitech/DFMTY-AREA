namespace Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

public interface IRiotSettingsProvider
{
    public string PlatformApiEndpoint { get; }
    public string RegionalApiEndpoint { get; }
    public string DataDragonApiEndpoint { get; }
    public string ApiKey { get; }
}
