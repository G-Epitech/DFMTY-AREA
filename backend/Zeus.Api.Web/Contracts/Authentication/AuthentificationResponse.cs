namespace Zeus.Api.Web.Contracts.Authentication;

public record AuthenticationResponse(
    string AccessToken,
    string RefreshToken);
