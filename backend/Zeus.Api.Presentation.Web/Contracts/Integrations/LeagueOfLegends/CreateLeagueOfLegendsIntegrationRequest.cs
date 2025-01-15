namespace Zeus.Api.Presentation.Web.Contracts.Integrations.LeagueOfLegends;

public record CreateLeagueOfLegendsIntegrationRequest(
    string GameName,
    string TagLine);
