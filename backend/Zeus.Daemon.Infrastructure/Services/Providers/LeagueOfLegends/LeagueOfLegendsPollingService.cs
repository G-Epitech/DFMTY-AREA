using Microsoft.Extensions.Logging;

using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Providers.LeagueOfLegends.Services;
using Zeus.Daemon.Domain.LeagueOfLegends;
using Zeus.Daemon.Domain.LeagueOfLegends.ValueObjects;

using Timer = System.Timers.Timer;

namespace Zeus.Daemon.Infrastructure.Services.Providers.LeagueOfLegends;

public class LeagueOfLegendsPollingService : ILeagueOfLegendsPollingService, IDaemonService
{
    private readonly ILeagueOfLegendsApiService _apiService;
    private readonly Timer _timer;
    private readonly Dictionary<RiotAccountId, LeagueOfLegendsMatchId> _cacheAccounts = new();
    private readonly ILogger _logger;

    private readonly List<Func<RiotAccountId, LeagueOfLegendsMatch, CancellationToken, Task>> _matchDetectedHandlers =
        new();

    public LeagueOfLegendsPollingService(ILeagueOfLegendsApiService apiService,
        ILogger<LeagueOfLegendsPollingService> logger)
    {
        _apiService = apiService;
        _logger = logger;

        _timer = new Timer(TimeSpan.FromSeconds(10));
        _timer.Elapsed += (sender, e) => _ = PollMatches();
        _timer.AutoReset = true;
    }

    public async Task<bool> RegisterRiotAccount(RiotAccountId accountId, CancellationToken cancellationToken)
    {
        if (_cacheAccounts.ContainsKey(accountId))
        {
            return true;
        }

        var lastMatchId = await _apiService.GetLastMatchAsync(accountId, cancellationToken);
        if (lastMatchId.IsError)
        {
            return false;
        }

        _cacheAccounts.Add(accountId, lastMatchId.Value);

        return true;
    }

    public Task UnregisterRiotAccount(RiotAccountId accountId, CancellationToken cancellationToken)
    {
        _cacheAccounts.Remove(accountId);

        return Task.CompletedTask;
    }

    public void RegisterNewMatchDetected(Func<RiotAccountId, LeagueOfLegendsMatch, CancellationToken, Task> handler)
    {
        _matchDetectedHandlers.Add(handler);
    }

    private async Task PollMatches()
    {
        foreach (KeyValuePair<RiotAccountId, LeagueOfLegendsMatchId> pair in _cacheAccounts)
        {
            var lastMatchId = await _apiService.GetLastMatchAsync(pair.Key);

            if (lastMatchId.IsError)
            {
                _logger.LogError("Error polling match for account {accountId}: {error}", pair.Key.Value,
                    lastMatchId.Errors);

                continue;
            }

            if (lastMatchId.Value == pair.Value)
            {
                continue;
            }

            await SendMatchDetected(pair.Key, lastMatchId.Value, CancellationToken.None);

            _cacheAccounts[pair.Key] = lastMatchId.Value;
        }
    }

    private async Task SendMatchDetected(RiotAccountId accountId, LeagueOfLegendsMatchId matchId,
        CancellationToken cancellationToken)
    {
        var match = await _apiService.GetMatchByIdAsync(matchId, cancellationToken);
        if (match.IsError)
        {
            _logger.LogError("Error getting match {matchId}: {error}", matchId.Value, match.Errors);

            return;
        }

        foreach (var handler in _matchDetectedHandlers)
        {
            await handler(accountId, match.Value, cancellationToken);
        }
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
}
