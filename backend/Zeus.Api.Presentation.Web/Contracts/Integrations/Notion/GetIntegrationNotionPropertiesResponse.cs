namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

public record GetIntegrationNotionPropertiesResponse(
    string Id,
    Uri AvatarUri,
    string Name,
    string Email,
    string WorkspaceName) : GetIntegrationPropertiesResponse;
