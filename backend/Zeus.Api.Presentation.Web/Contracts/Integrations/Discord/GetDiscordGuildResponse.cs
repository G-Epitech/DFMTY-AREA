namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;

public record GetDiscordGuildResponse(
    string Id,
    string Name,
    Uri IconUri,
    uint ApproximateMemberCount,
    bool Linked);
