using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord.ValueObjects;

public class DiscordUserTokens : ValueObject
{
    public AccessToken AccessToken { get; }
    public RefreshToken RefreshToken { get; }
    public string TokenType { get; }
    public uint ExpiresIn { get; }

    public DiscordUserTokens(AccessToken accessToken, RefreshToken refreshToken, string tokenType,
        uint expiresIn)
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
