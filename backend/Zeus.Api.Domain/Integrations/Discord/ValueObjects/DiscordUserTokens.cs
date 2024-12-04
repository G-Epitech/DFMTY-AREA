using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord.ValueObjects;

public class DiscordUserTokens : ValueObject
{
    AccessToken AccessToken { get; }
    RefreshToken RefreshToken { get; }
    string TokenType { get; }
    uint ExpiresIn { get; }

    public DiscordUserTokens(AccessToken accessToken, RefreshToken refreshToken, string tokenType,
        uint responseContentExpiresIn, uint expiresIn)
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
