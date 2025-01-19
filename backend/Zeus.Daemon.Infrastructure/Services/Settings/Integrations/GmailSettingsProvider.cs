using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Infrastructure.Settings.Integrations;

namespace Zeus.Daemon.Infrastructure.Services.Settings.Integrations;

public class GmailSettingsProvider : IGmailSettingsProvider
{
    public GmailSettingsProvider(GmailSettings settings)
    {
        MessagesApiEndpoint = settings.MessagesApiEndpoint;
    }

    public string MessagesApiEndpoint { get; }
}
