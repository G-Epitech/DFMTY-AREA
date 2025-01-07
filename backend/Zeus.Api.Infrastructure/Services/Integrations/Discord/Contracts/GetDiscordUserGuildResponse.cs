namespace Zeus.Api.Infrastructure.Services.Integrations.Discord.Contracts;

public record GetDiscordUserGuildResponse(
    string Id,
    string Name,
    string? Icon,
    string? Banner,
    bool Owner,
    List<string> Features,
    uint ApproximateMemberCount,
    uint ApproximatePresenceCount
);
