namespace Zeus.Api.Application.Users.Query;

public record GetUserQueryResult(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Picture);
