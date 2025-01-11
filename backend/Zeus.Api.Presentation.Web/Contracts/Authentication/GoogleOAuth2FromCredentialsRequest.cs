namespace Zeus.Api.Presentation.Web.Contracts.Authentication;

public record GoogleOAuth2FromCredentialsRequest(
    string AccessToken,
    string RefreshToken,
    string TokenType);
