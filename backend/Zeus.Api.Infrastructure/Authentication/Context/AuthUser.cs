namespace Zeus.Api.Infrastructure.Authentication.Context;

public record AuthUser(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
