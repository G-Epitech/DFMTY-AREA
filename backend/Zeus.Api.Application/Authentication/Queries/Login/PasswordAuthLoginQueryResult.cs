using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public record PasswordAuthLoginQueryResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken);
