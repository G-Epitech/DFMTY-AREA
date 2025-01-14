using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCredentials;

public record GoogleAuthFromCredentialsCommandResult(
    bool IsRegistered,
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
