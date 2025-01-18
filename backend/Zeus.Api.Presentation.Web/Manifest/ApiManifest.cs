using Zeus.Common.Domain.ProvidersSettings;

namespace Zeus.Api.Presentation.Web.Manifest;

public sealed class ApiManifest
{
    public required ApiManifestClient Client { get; init; }
    public required ApiManifestServer Server { get; init; }
}
