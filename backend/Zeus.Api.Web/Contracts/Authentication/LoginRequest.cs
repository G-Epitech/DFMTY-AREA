namespace Zeus.Api.Web.Contracts.Authentication;

public record LoginRequest(
    string Email,
    string Password);
