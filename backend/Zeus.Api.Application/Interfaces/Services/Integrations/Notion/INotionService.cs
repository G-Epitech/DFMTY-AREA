using ErrorOr;

using Zeus.Api.Domain.Integrations.Notion;
using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Interfaces.Services.Integrations.Notion;

public interface INotionService
{
    /// <summary>
    /// Get notion tokens from oauth2 code.
    /// </summary>
    /// <param name="code">The code provided by the service</param>
    /// <returns>Notion workspace tokens</returns>
    public Task<ErrorOr<NotionWorkspaceTokens>> GetTokensFromOauth2Async(string code);

    /// <summary>
    /// Get bot infos from workspace access token.
    /// </summary>
    /// <param name="accessToken">The access token provided by oauth2</param>
    /// <returns>The notion bot with workspace and owner props</returns>
    public Task<ErrorOr<NotionBot>> GetBotAsync(AccessToken accessToken);
}
