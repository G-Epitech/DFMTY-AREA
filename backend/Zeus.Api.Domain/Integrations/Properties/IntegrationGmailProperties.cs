namespace Zeus.Api.Domain.Integrations.Properties;

public record IntegrationGmailProperties(
    string Id,
    string Email,
    string GivenName,
    string FamilyName,
    string DisplayName,
    Uri AvatarUri) : IntegrationProperties;
