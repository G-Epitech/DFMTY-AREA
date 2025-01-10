namespace Zeus.Api.Domain.Integrations.Properties;

public record IntegrationNotionProperties(
    string Id,
    Uri AvatarUri,
    string Name,
    string Email,
    string WorkspaceName) : IntegrationProperties;
