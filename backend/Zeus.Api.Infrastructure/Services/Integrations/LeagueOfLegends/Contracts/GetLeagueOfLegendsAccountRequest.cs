namespace Zeus.Api.Infrastructure.Services.Integrations.LeagueOfLegends.Contracts;

public record GetLeagueOfLegendsAccountRequest(
    string Puuid,
    string GameName,
    string TagLine);
