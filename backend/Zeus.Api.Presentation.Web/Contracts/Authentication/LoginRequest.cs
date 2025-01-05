namespace Zeus.Api.Presentation.Web.Contracts.Authentication;

public record LoginRequest(
    string Email,
    string Password);
