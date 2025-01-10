using ErrorOr;

using MediatR;

using Zeus.Api.Domain.OAuth2.Google;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.GoogleRegister;

public record GoogleAuthRegisterCommand(
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    GoogleUser GoogleUser) : IRequest<ErrorOr<GoogleAuthRegisterCommandResult>>;
