using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuth;

public record GoogleAuthCommand(
    string Code) : IRequest<ErrorOr<GoogleAuthCommandResult>>;
