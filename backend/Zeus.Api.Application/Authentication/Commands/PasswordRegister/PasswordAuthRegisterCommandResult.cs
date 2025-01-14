using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.PasswordRegister;

public record PasswordAuthRegisterCommandResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
