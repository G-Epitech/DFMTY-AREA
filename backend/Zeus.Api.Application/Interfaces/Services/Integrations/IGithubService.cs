using ErrorOr;

using Zeus.Api.Domain.Integrations.Github;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IGithubService
{
    /// <summary>
    /// Get github tokens from oauth2 code.
    /// </summary>
    /// <param name="code">The code provided by the service</param>
    /// <returns>Github tokens</returns>
    public Task<ErrorOr<GithubTokens>> GetTokensFromOauth2Async(string code);

    /// <summary>
    /// Get user info from access token.
    /// </summary>
    /// <param name="accessToken">The user access token</param>
    /// <returns>The github user</returns>
    public Task<ErrorOr<GithubUser>> GetUserAsync(AccessToken accessToken);
}
