using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.Providers.Notion;

namespace Zeus.Daemon.Application.Providers.Notion.Services;

public interface INotionPollingService
{
    /// <summary>
    /// Register handler for new database detected.
    /// </summary>
    /// <param name="automationId">The automation id</param>
    /// <param name="accessToken">The notion access token</param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    public Task<bool> RegisterNewDatabaseDetected(AutomationId automationId, AccessToken accessToken,
        Func<AutomationId, NotionDatabase, CancellationToken, Task> handler, CancellationToken cancellationToken);

    /// <summary>
    /// Register handler for new page detected.
    /// </summary>
    /// <param name="automationId">The automation id</param>
    /// <param name="accessToken">The notion access token</param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    public Task<bool> RegisterNewPageDetected(AutomationId automationId, AccessToken accessToken,
        Func<AutomationId, NotionPage, CancellationToken, Task> handler, CancellationToken cancellationToken);

    /// <summary>
    /// Register handler for remove page detected.
    /// </summary>
    /// <param name="automationId">The automation id</param>
    /// <param name="accessToken">The notion access token</param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    public Task<bool> RegisterRemovePageDetected(AutomationId automationId, AccessToken accessToken,
        Func<AutomationId, NotionPage, CancellationToken, Task> handler, CancellationToken cancellationToken);

    /// <summary>
    /// Unregister handler for new database detected.
    /// </summary>
    /// <param name="automationId"></param>
    public void UnregisterNewDatabaseDetected(AutomationId automationId);

    /// <summary>
    /// Unregister handler for new page detected.
    /// </summary>
    /// <param name="automationId"></param>
    public void UnregisterNewPageDetected(AutomationId automationId);

    /// <summary>
    /// Unregister handler for remove page detected.
    /// </summary>
    /// <param name="automationId"></param>
    public void UnregisterRemovePageDetected(AutomationId automationId);
}
