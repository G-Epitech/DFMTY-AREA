namespace Zeus.Api.Infrastructure.Services.Integrations.Discord.Contracts;

public record GetDiscordGuildChannelResponse(
    string Id,
    int Type,
    string? GuildId,
    int? Position,
    string? Name,
    string? Topic,
    bool? Nsfw,
    string? OwnerId);
