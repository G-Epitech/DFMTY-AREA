namespace Zeus.Api.Presentation.Web.Manifest;

public class ApiManifestServer
{
    public long CurrentTime => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public required List<ApiManifestService> Services { get; init; }
}
