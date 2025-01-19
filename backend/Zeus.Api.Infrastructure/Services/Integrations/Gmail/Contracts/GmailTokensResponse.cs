namespace Zeus.Api.Infrastructure.Services.Integrations.Gmail.Contracts;

public record GmailTokensResponse(
    string AccessToken,
    string TokenType,
    uint ExpiresIn,
    string RefreshToken,
    string Scope);
