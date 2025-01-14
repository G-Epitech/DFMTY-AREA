namespace Zeus.Api.Infrastructure.Services.Integrations.LeagueOfLegends.Contracts;

public record GetLeagueOfLegendsSummonerResponse(
    string Id,
    string AccountId,
    string Puuid,
    uint ProfileIconId,
    long RevisionDate,
    long SummonerLevel);
