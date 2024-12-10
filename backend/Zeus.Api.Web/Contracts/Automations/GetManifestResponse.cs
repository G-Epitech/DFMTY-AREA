namespace Zeus.Api.Web.Contracts.Automations;

public record GetManifestServiceResponse(
    string Name,
    Uri IconUri,
    string Color,
    Dictionary<string, GetManifestTriggersResponse> Triggers,
    Dictionary<string, GetManifestActionsResponse> Actions);

public record GetManifestTriggersResponse(
    string Name,
    string Description,
    string Icon,
    Dictionary<string, GetManifestTriggersPropertiesResponse> Properties,
    Dictionary<string, GetManifestTriggersFactsResponse> Facts);

public record GetManifestTriggersPropertiesResponse(
    string Name,
    string Description,
    string Type);

public record GetManifestTriggersFactsResponse(
    string Name,
    string Description,
    string Type);

public record GetManifestActionsResponse(
    string Name,
    string Description,
    string Icon,
    Dictionary<string, GetManifestActionsPropertiesResponse> Properties,
    Dictionary<string, GetManifestActionsFactsResponse> Facts);

public record GetManifestActionsPropertiesResponse(
    string Name,
    string Description,
    string Type);

public record GetManifestActionsFactsResponse(
    string Name,
    string Description,
    string Type);
