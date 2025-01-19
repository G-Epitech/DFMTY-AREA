namespace Zeus.Api.Infrastructure.Services.Integrations.Gmail.Contracts;

public sealed record GetGmailUserResponse(
    string FamilyName,
    string GivenName,
    string Name,
    string Picture,
    string Email,
    bool VerifiedEmail,
    string Id);
