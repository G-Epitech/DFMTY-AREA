using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

public class NotionSettingsProvider : INotionSettingsProvider
{
    public NotionSettingsProvider(NotionSettings settings)
    {
        ApiEndpoint = settings.ApiEndpoint;
    }

    public string ApiEndpoint { get; }
}
