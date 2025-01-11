using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCode;

public record GoogleAuthFromCodeCommand(
    string Code) : IRequest<ErrorOr<GoogleAuthFromCodeCommandResult>>;
