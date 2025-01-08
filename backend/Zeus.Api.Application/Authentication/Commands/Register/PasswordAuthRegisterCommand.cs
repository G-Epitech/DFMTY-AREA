using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public record PasswordAuthRegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<ErrorOr<PasswordAuthRegisterCommandResult>>;
