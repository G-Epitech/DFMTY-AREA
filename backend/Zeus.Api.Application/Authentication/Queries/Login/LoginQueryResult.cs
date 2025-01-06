using Zeus.Common.Domain.Authentication.ValueObjects;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public record LoginQueryResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken);
