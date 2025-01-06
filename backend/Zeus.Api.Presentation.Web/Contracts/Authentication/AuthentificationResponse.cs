namespace Zeus.Api.Presentation.Web.Contracts.Authentication;

public record AuthenticationResponse(
    string AccessToken,
    string RefreshToken);
