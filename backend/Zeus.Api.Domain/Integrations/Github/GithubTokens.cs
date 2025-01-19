using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Domain.Integrations.Github;

public class GithubTokens
{
    public GithubTokens(AccessToken accessToken, string tokenType)
    {
        AccessToken = accessToken;
        TokenType = tokenType;
    }

    public AccessToken AccessToken { get; private set; }
    public string TokenType { get; private set; }
}
