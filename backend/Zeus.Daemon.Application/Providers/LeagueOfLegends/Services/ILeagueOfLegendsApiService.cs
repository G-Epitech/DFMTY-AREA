using ErrorOr;

using Zeus.Daemon.Domain.LeagueOfLegends;
using Zeus.Daemon.Domain.LeagueOfLegends.ValueObjects;

namespace Zeus.Daemon.Application.Providers.LeagueOfLegends.Services;

public interface ILeagueOfLegendsApiService
{
    /// <summary>
    /// Get the last match id of a specific summoner by riot account id
    /// </summary>
    /// <param name="accountId">Riot account id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The last match id</returns>
    public Task<ErrorOr<LeagueOfLegendsMatchId>> GetLastMatchAsync(RiotAccountId accountId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a match by id
    /// </summary>
    /// <param name="matchId">The match id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The match corresponding to the id</returns>
    public Task<ErrorOr<LeagueOfLegendsMatch>> GetMatchByIdAsync(LeagueOfLegendsMatchId matchId,
        CancellationToken cancellationToken = default);
}
