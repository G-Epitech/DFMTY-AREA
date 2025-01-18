namespace Zeus.Api.Presentation.Web.Manifest;

public class ApiManifestService
{
    public required string Name { get; init; }
    public required List<ApiManifestServiceElement> Actions { get; init; }
    public required List<ApiManifestServiceElement> Reactions { get; init; }
}
