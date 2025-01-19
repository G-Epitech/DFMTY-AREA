using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Domain.Integrations.Gmail.ValueObjects;

public class GmailUserTokens : ValueObject
{
    public GmailUserTokens(AccessToken accessToken, RefreshToken refreshToken, string tokenType,
        uint expiresIn)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
    }

    public AccessToken AccessToken { get; }
    public RefreshToken RefreshToken { get; }
    public string TokenType { get; }
    public uint ExpiresIn { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AccessToken;
        yield return RefreshToken;
        yield return TokenType;
        yield return ExpiresIn;
    }
}
