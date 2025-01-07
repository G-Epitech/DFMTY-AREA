namespace Zeus.Api.Application.Integrations.Query.GetDiscordGuilds;

public record GetDiscordGuildQueryResult(
    string Id,
    string Name,
    Uri IconUri,
    uint ApproximateMemberCount,
    bool Linked);
