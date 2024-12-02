namespace Zeus.Api.Infrastructure.Authentication.Context;

public record AuthUser(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName);
