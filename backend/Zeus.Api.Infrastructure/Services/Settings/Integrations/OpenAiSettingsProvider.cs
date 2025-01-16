using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Infrastructure.Settings.Integrations;

namespace Zeus.Api.Infrastructure.Services.Settings.Integrations;

public class OpenAiSettingsProvider : IOpenAiSettingsProvider
{
    public OpenAiSettingsProvider(OpenAiSettings settings)
    {
        ApiEndpoint = settings.ApiEndpoint;
    }

    public string ApiEndpoint { get; }
}
