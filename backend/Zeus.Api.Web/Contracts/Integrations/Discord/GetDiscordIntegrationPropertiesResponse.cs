namespace Zeus.Api.Web.Contracts.Integrations.Discord;

public record GetDiscordIntegrationPropertiesResponse(
    string Id,
    string Email,
    string Username,
    string DisplayName,
    Uri AvatarUri,
    string[] Flags) : GetIntegrationPropertiesResponse;
