namespace Zeus.Api.Infrastructure.Services.OAuth2.Google.Contracts;

public record GoogleOauth2TokenResponse(
    string AccessToken,
    string TokenType,
    uint ExpiresIn,
    string RefreshToken,
    string Scope);
