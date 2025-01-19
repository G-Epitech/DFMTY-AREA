using ErrorOr;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Application.Providers.Gmail.Services.GmailApiFilters;
using Zeus.Daemon.Domain.Providers.Gmail.Entities;
using Zeus.Daemon.Domain.Providers.Gmail.ValueObjects;

namespace Zeus.Daemon.Application.Providers.Gmail.Services;

public interface IGmailApiService
{
    /// <summary>
    /// Get messages from the user's mailbox
    /// </summary>
    /// <param name="accessToken">Access token of the user</param>
    /// <param name="userId">The user id</param>
    /// <param name="filters">The filters to apply to the query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The email ids matching the query</returns>
    public Task<ErrorOr<List<GmailMessageId>>> GetMessagesAsync(AccessToken accessToken, GmailUserId userId,
        GetGmailMessagesFilters? filters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the message from the user's mailbox
    /// </summary>
    /// <param name="accessToken">Access token of the user</param>
    /// <param name="messageId">The message id</param>
    /// <param name="userId">The user id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The email matching the query</returns>
    public Task<ErrorOr<GmailMessage>> GetMessageAsync(AccessToken accessToken, GmailMessageId messageId, GmailUserId userId,
        CancellationToken cancellationToken = default);
}
