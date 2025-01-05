namespace Zeus.Api.Presentation.Web.Contracts.Users;

public record GetUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Picture);
