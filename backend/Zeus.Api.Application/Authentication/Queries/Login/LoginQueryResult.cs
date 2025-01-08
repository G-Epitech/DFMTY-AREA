using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public record LoginQueryResult(
    AccessToken AccessToken,
    RefreshToken RefreshToken);
