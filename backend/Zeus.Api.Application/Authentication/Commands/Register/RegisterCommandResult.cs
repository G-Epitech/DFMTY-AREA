using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public record RegisterCommandResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
