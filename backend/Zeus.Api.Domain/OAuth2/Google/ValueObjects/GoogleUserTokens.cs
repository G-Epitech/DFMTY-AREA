using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Domain.OAuth2.Google.ValueObjects;

public sealed class GoogleUserTokens : ValueObject
{
    public AccessToken AccessToken { get; }
    public RefreshToken RefreshToken { get; }
    public string TokenType { get; }
    public uint ExpiresIn { get; }

    public GoogleUserTokens(AccessToken accessToken, RefreshToken refreshToken, string tokenType, uint expiresIn)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AccessToken;
        yield return RefreshToken;
        yield return TokenType;
        yield return ExpiresIn;
    }
}
