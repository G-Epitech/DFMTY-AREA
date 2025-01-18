using ErrorOr;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Domain.Providers.Notion;

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
}
