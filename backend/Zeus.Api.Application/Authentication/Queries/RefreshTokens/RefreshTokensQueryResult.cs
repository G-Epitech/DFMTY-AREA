using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Authentication.Queries.RefreshTokens;

public record RefreshTokensQueryResult(AccessToken AccessToken,
    RefreshToken RefreshToken);
