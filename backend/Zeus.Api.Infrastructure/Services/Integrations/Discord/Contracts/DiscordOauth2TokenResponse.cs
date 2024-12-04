namespace Zeus.Api.Infrastructure.Services.Integrations.Discord.Contracts;

public record DiscordOauth2TokenResponse(
    string AccessToken,
    string TokenType,
    uint ExpiresIn,
    string RefreshToken,
    string Scope);
