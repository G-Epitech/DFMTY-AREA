using ErrorOr;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Domain.Providers.Notion;
using Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Notion.Services;

public interface INotionApiService
{
    /// <summary>
    /// Get databases of the workspace.
    /// </summary>
    /// <param name="accessToken">The workspace bot access token</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The list of databases in the workspace</returns>
    public Task<ErrorOr<List<NotionDatabase>>> GetWorkspaceDatabasesAsync(AccessToken accessToken,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get pages of the workspace.
    /// </summary>
    /// <param name="accessToken">The workspace bot access token</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The list of pages in the workspace</returns>
    public Task<ErrorOr<List<NotionPage>>> GetWorkspacePagesAsync(AccessToken accessToken,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new notion database
    /// </summary>
    /// <param name="accessToken">The workspace bot access token</param>
    /// <param name="parentId">The parent page id of the database</param>
    /// <param name="title">The title of the database</param>
    /// <param name="icon">The icon, can be emoji or link</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ErrorOr<NotionDatabase>> CreateDatabaseInPageAsync(AccessToken accessToken, NotionPageId parentId,
        string title, string icon, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new notion page
    /// </summary>
    /// <param name="accessToken">The workspace bot access token</param>
    /// <param name="parentId">The parent page id of the page</param>
    /// <param name="title">The title of the page</param>
    /// <param name="icon">The icon, can be emoji or link</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ErrorOr<NotionPage>> CreatePageInPageAsync(AccessToken accessToken, NotionPageId parentId,
        string title, string icon, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new notion page
    /// </summary>
    /// <param name="accessToken">The workspace bot access token</param>
    /// <param name="parentId">The parent database id of the page</param>
    /// <param name="titleParamName">The name of the parameter where the title of the row is displayed</param>
    /// <param name="title">The title of the page</param>
    /// <param name="icon">The icon, can be emoji or link</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ErrorOr<NotionPage>> CreatePageInDatabaseAsync(AccessToken accessToken, NotionDatabaseId parentId,
        string titleParamName, string title, string icon, CancellationToken cancellationToken = default);
}
