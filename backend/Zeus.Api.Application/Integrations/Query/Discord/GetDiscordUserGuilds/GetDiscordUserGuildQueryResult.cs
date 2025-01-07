namespace Zeus.Api.Application.Integrations.Query.Discord.GetDiscordUserGuilds;

public record GetDiscordUserGuildQueryResult(
    string Id,
    string Name,
    Uri IconUri,
    uint ApproximateMemberCount,
    bool Linked);
