using Zeus.Common.Domain.ProvidersSettings;

namespace Zeus.Api.Presentation.Web.Manifest;

public sealed class ApiManifestProvider
{
    private readonly ProvidersSettings _providersSettings;
    private readonly string _clientHost;

    public ApiManifestProvider(ProvidersSettings providersSettings, IConfiguration configuration)
    {
        _providersSettings = providersSettings;
        var clientHost = configuration.GetSection("Manifest:ClientHost").Value;

        if (string.IsNullOrEmpty(clientHost))
        {
            throw new InvalidOperationException("Client host is not configured");
        }
        _clientHost = clientHost;
    }

    public ApiManifest GetManifest()
    {
        var services = _providersSettings.ToDictionary();

        return new ApiManifest
        {
            Client = new ApiManifestClient { Host = _clientHost },
            Server = new ApiManifestServer
            {
                Services = services.Select(s => new ApiManifestService
                {
                    Name = s.Key,
                    Actions = s.Value.Triggers.Select(a => new ApiManifestServiceElement { Name = a.Key, Description = a.Value.Description }).ToList(),
                    Reactions = s.Value.Actions.Select(r => new ApiManifestServiceElement { Name = r.Key, Description = r.Value.Description }).ToList()
                }).ToList()
            }
        };
    }
}
