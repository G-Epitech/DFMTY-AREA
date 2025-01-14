using ErrorOr;

using Zeus.Api.Domain.Integrations.LeagueOfLegends;
using Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface ILeagueOfLegendsService
{
    /// <summary>
    /// Get account by riot name
    /// </summary>
    /// <param name="gameName">The riot game name</param>
    /// <param name="tagLine">THe riot tag without the #</param>
    /// <returns>Riot account</returns>
    public Task<ErrorOr<RiotAccount>> GetRiotAccountByNameAsync(string gameName, string tagLine);

    /// <summary>
    /// Get account by riot id
    /// </summary>
    /// <param name="riotAccountId">Riot account id (PUUID)</param>
    /// <returns>Riot account</returns>
    public Task<ErrorOr<RiotAccount>> GetRiotAccountByIdAsync(RiotAccountId riotAccountId);

    /// <summary>
    /// Get summoner by riot account id
    /// </summary>
    /// <param name="riotAccountId">Riot account id (PUUID)</param>
    /// <returns>League of legends summoner</returns>
    public Task<ErrorOr<LeagueOfLegendsSummoner>> GetSummonerByRiotAccountId(RiotAccountId riotAccountId);

    /// <summary>
    /// Get summoner profile icon uri
    /// </summary>
    /// <param name="profileIconId">Summoner icon id</param>
    /// <returns>Uri to summoner icon</returns>
    public Uri GetSummonerProfileIconUri(LeagueOfLegendsSummonerProfileIconId profileIconId);
}
