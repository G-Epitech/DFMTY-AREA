using Zeus.Api.Domain.Authentication.ValueObjects;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public record RegisterCommandResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken,
    Guid UserId);
