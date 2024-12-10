namespace Zeus.Api.Domain.Integrations.Properties;

public record IntegrationDiscordProperties(
    string Id,
    string Email,
    string Username,
    string DisplayName,
    Uri AvatarUri,
    string[] Flags) : IntegrationProperties;
