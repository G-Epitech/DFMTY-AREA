using ErrorOr;

using MediatR;

using Zeus.Api.Application.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<ErrorOr<AuthenticationResult>>;
