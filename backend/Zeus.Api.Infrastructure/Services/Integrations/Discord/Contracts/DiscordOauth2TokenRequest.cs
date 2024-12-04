namespace Zeus.Api.Infrastructure.Services.Integrations.Discord.Contracts;

public record DiscordOauth2TokenRequest(
    string GrantType,
    string Code,
    string RedirectUri);
