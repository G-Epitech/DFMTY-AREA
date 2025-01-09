using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuth;

public record GoogleAuthCommandResult(
    bool IsRegistered,
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
