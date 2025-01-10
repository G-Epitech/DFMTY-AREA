namespace Zeus.Api.Infrastructure.Services.OAuth2.Google.Contracts;

public record GetGoogleUserResponse(
    string Id,
    string Email,
    bool VerifiedEmail,
    string Name,
    string GivenName,
    string FamilyName,
    string Picture);
