using Zeus.Daemon.Domain.LeagueOfLegends;
using Zeus.Daemon.Domain.LeagueOfLegends.ValueObjects;

namespace Zeus.Daemon.Application.Providers.LeagueOfLegends.Services;

public interface ILeagueOfLegendsPollingService
{
    /// <summary>
    /// Register a Riot account to be polled for match history.
    /// </summary>
    /// <param name="accountId">Account id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> RegisterRiotAccount(RiotAccountId accountId, CancellationToken cancellationToken);

    /// <summary>
    /// Unregister a Riot account from being polled for match history.
    /// </summary>
    /// <param name="accountId">Account id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task UnregisterRiotAccount(RiotAccountId accountId, CancellationToken cancellationToken);

    /// <summary>
    /// Register a new handler when a new match is detected in any registered accounts.
    /// </summary>
    /// <param name="handler">The handler method</param>
    public void RegisterNewMatchDetected(Func<RiotAccountId, LeagueOfLegendsMatch, CancellationToken, Task> handler);
}
