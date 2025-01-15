namespace Zeus.Api.Domain.Integrations.Properties;

public record IntegrationLeagueOfLegendsProperties(
    string RiotAccountId,
    string RiotGameName,
    string RiotTagLine,
    string SummonerId,
    string SummonerAccountId,
    Uri SummonerProfileIcon) : IntegrationProperties;
