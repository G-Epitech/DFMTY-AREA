namespace Zeus.Api.Application.Integrations.Query.GetIntegration.Results;

public record GetDiscordIntegrationPropertiesQueryResult(
    string Id,
    string Email,
    string Username,
    string DisplayName,
    Uri AvatarUri,
    string[] Flags) : GetIntegrationPropertiesQueryResult;
