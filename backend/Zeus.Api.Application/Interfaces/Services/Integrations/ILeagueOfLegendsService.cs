using ErrorOr;

using Zeus.Api.Domain.Integrations.LeagueOfLegends;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface ILeagueOfLegendsService
{
    /// <summary>
    /// Get account by riot id
    /// </summary>
    /// <param name="gameName">The riot game name</param>
    /// <param name="tagLine">THe riot tag without the #</param>
    /// <returns>League of legends account</returns>
    public Task<ErrorOr<LeagueOfLegendsAccount>> GetAccountByRiotIdAsync(string gameName, string tagLine);
}
