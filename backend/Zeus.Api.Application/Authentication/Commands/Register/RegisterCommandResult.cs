using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public record RegisterCommandResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
