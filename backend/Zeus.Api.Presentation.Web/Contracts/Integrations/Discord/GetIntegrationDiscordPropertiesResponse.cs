namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;

public record GetIntegrationDiscordPropertiesResponse(
    string Id,
    string Email,
    string Username,
    string DisplayName,
    Uri AvatarUri,
    string[] Flags) : GetIntegrationPropertiesResponse;
