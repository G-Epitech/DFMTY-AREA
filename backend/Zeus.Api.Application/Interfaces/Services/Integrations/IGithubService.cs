using ErrorOr;

using Zeus.Api.Domain.Integrations.Github;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IGithubService
{
    /// <summary>
    /// Get github tokens from oauth2 code.
    /// </summary>
    /// <param name="code">The code provided by the service</param>
    /// <returns>Github tokens</returns>
    public Task<ErrorOr<GithubTokens>> GetTokensFromOauth2Async(string code);
}
