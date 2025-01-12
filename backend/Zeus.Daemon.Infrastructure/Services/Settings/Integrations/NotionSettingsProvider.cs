using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Integrations;

namespace Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

public class NotionSettingsProvider : INotionSettingsProvider
{
    public string ApiEndpoint { get; }

    public NotionSettingsProvider(NotionSettings settings)
    {
        ApiEndpoint = settings.ApiEndpoint;
    }
}
