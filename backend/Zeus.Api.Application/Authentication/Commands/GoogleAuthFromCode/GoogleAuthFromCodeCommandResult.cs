using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCode;

public record GoogleAuthFromCodeCommandResult(
    bool IsRegistered,
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
