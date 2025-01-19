using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Domain.Providers.Gmail;
using Zeus.Daemon.Domain.Providers.Gmail.ValueObjects;
using Zeus.Daemon.Domain.Providers.Notion;

namespace Zeus.Daemon.Application.Providers.Gmail.Services;

/// <summary>
/// Event handler for when new messages are received
/// </summary>
public delegate Task OnMessagesReceivedHandler(GmailWatchingId watchingId, List<GmailMessageId> messageIds,
    CancellationToken cancellationToken);

public interface IGmailPollingService
{
    /// <summary>
    /// Watch for new email received
    /// </summary>
    /// <param name="accessToken">The access token</param>
    /// <param name="userId">The user id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="watchingId">Watching id</param>
    public bool WatchNewEmailReceived(
        AccessToken accessToken,
        GmailUserId userId,
        CancellationToken cancellationToken,
        [NotNullWhen(true)] out GmailWatchingId? watchingId);

    /// <summary>
    /// Unwatch for new email received
    /// </summary>
    /// <param name="watchingId">Watching id to unwatch</param>
    public void UnwatchEmailReceived(GmailWatchingId watchingId);

    /// <summary>
    /// Register a handler for when new messages are received
    /// </summary>
    /// <param name="handler">Handler to register</param>
    public void RegisterOnMessagesReceivedHandler(OnMessagesReceivedHandler handler);

    /// <summary>
    /// Unregister a handler for when new messages are received
    /// </summary>
    /// <param name="handler">Handler to unregister</param>
    public void UnregisterOnMessagesReceivedHandler(OnMessagesReceivedHandler handler);
}
