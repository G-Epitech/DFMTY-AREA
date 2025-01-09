using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.GoogleRegister;

public record GoogleAuthRegisterCommandResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
