using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.Gmail.Services;
using Zeus.Daemon.Application.Providers.Gmail.Services.GmailApiFilters;
using Zeus.Daemon.Domain.Providers.Gmail;
using Zeus.Daemon.Domain.Providers.Gmail.ValueObjects;

using Timer = System.Timers.Timer;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Gmail;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class GmailPollingService : IGmailPollingService, IDaemonService
{
    private readonly Timer _timer;
    private readonly ILogger _logger;
    private readonly IGmailApiService _gmailApiService;
    private readonly List<OnMessagesReceivedHandler> _onMessagesReceivedHandlers = [];
    private readonly Dictionary<GmailWatchingId, GetMessagesWatchParameters> _messagesReceivedWatchers = new();

    public GmailPollingService(IGmailApiService gmailApiService, ILogger<GmailPollingService> logger)
    {
        _gmailApiService = gmailApiService;
        _logger = logger;
        _timer = new Timer(TimeSpan.FromSeconds(4));
        _timer.Elapsed += (sender, e) => _ = PollEmailsReceived();
        _timer.AutoReset = true;
    }

    public bool WatchNewEmailReceived(
        AccessToken accessToken,
        GmailUserId userId,
        CancellationToken cancellationToken,
        [NotNullWhen(true)] out GmailWatchingId? watchingId)
    {
        var id = GmailWatchingId.CreateUnique();
        var filters = new GetGmailMessagesFilters { After = DateTime.UtcNow };
        var parameters = new GetMessagesWatchParameters { AccessToken = accessToken.Value, ClientId = userId.Value, Filters = filters };

        if (_messagesReceivedWatchers.TryAdd(id, parameters))
        {
            watchingId = id;
            return true;
        }
        watchingId = null;
        return false;
    }

    public void UnwatchEmailReceived(GmailWatchingId watchingId)
    {
        _messagesReceivedWatchers.Remove(watchingId);
    }

    private async Task PollEmailsReceived()
    {
        try
        {
            foreach ((GmailWatchingId watchingId, GetMessagesWatchParameters parameters) in _messagesReceivedWatchers)
            {
                var filters = parameters.Filters.Copy();
                lock (parameters.Lock)
                {
                    parameters.Filters.After = DateTime.UtcNow;
                }

                var res = await _gmailApiService.GetMessagesAsync(new AccessToken(parameters.AccessToken), new GmailUserId(parameters.ClientId), filters);

                if (res.IsError)
                {
                    _logger.LogError("Error while polling emails received: {Error}", res.FirstError.Description);
                }
                else if (res.Value.Count > 0)
                {
                    foreach (var handler in _onMessagesReceivedHandlers)
                    {
                        _ = handler(watchingId, res.Value, CancellationToken.None);
                    }
                }
                else
                {
                    _logger.LogDebug("No new emails received");
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Error while polling emails received: {Error}", e.Message);
        }
    }


    public void RegisterOnMessagesReceivedHandler(OnMessagesReceivedHandler handler)
    {
        _onMessagesReceivedHandlers.Add(handler);
    }

    public void UnregisterOnMessagesReceivedHandler(OnMessagesReceivedHandler handler)
    {
        _onMessagesReceivedHandlers.Remove(handler);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        _timer.Stop();
        return Task.CompletedTask;
    }

    private record GetMessagesWatchParameters
    {
        public required string ClientId { get; set; }
        public required string AccessToken { get; set; }
        public required GetGmailMessagesFilters Filters { get; set; }
        public Lock Lock { get; set; } = new();
    }
}
