using Zeus.Api.Domain.Authentication.ValueObjects;

namespace Zeus.Api.Application.Authentication.Common;

public record AuthenticationResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken);
