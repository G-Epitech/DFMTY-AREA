namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Gmail;

public record GetIntegrationGmailPropertiesResponse(
    string Id,
    string Email,
    string GivenName,
    string FamilyName,
    string DisplayName,
    string AvatarUri) : GetIntegrationPropertiesResponse;
