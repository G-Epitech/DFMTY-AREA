namespace Zeus.Api.Presentation.Web.Contracts.Integrations.LeagueOfLegends;

public record GetIntegrationLeagueOfLegendsPropertiesResponse(
    string RiotAccountId,
    string RiotGameName,
    string RiotTagLine,
    string SummonerId,
    string SummonerAccountId,
    Uri SummonerProfileIcon) : GetIntegrationPropertiesResponse;
