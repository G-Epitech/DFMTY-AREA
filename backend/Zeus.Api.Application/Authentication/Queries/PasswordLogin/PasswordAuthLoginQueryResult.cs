using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Queries.PasswordLogin;

public record PasswordAuthLoginQueryResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken);
