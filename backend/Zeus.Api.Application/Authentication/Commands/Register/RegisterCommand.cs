using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<ErrorOr<RegisterCommandResult>>;