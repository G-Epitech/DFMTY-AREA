namespace Zeus.Api.Infrastructure.Services.Integrations.LeagueOfLegends.Contracts;

public record GetLeagueOfLegendsAccountResponse(
    string Puuid,
    string GameName,
    string TagLine);
