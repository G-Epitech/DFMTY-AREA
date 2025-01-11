using ErrorOr;

using MediatR;

using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCredentials;

public record GoogleAuthFromCredentialsCommand(
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    string TokenType) : IRequest<ErrorOr<GoogleAuthFromCredentialsCommandResult>>;
