namespace Zeus.Daemon.Infrastructure.Services.Providers.LeagueOfLegends.Contracts;

public record GetLeagueOfLegendsMatchResult(
    GetLeagueOfLegendsMatchMetadata Metadata,
    GetLeagueOfLegendsMatchInfo Info);

public record GetLeagueOfLegendsMatchMetadata(
    string DataVersion,
    string MatchId,
    List<string> Participants);

public record GetLeagueOfLegendsMatchInfo(
    string GameType,
    long GameStartTimestamp,
    long GameEndTimestamp,
    long GameDuration,
    List<GetLeagueOfLegendsMatchParticipant> Participants);

public record GetLeagueOfLegendsMatchParticipant(
    string Puuid,
    bool Win,
    int ChampionId,
    string ChampionName,
    GetLeagueOfLegendsMatchParticipantChallenges Challenges);

public record GetLeagueOfLegendsMatchParticipantChallenges(
    float Kda);
