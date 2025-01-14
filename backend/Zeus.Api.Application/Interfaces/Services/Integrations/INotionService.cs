using ErrorOr;

using Zeus.Api.Domain.Integrations.Notion;
using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

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

    /// <summary>
    /// Get databases of the workspace.
    /// </summary>
    /// <param name="accessToken">The workspace bot access token</param>
    /// <returns>The list of databases in the workspace</returns>
    public Task<ErrorOr<List<NotionDatabase>>> GetWorkspaceDatabasesAsync(AccessToken accessToken);

    /// <summary>
    /// Get pages of the workspace.
    /// </summary>
    /// <param name="accessToken">The workspace bot access token</param>
    /// <returns>The list of pages in the workspace</returns>
    public Task<ErrorOr<List<NotionPage>>> GetWorkspacePagesAsync(AccessToken accessToken);
}
