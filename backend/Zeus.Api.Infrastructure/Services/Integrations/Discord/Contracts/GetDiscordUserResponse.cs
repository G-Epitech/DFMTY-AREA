namespace Zeus.Api.Infrastructure.Services.Integrations.Discord.Contracts;

public record GetDiscordUserResponse(
    string Id,
    string Username,
    string Discriminator,
    string? GlobalName,
    string? Avatar,
    bool? Bot,
    bool? System,
    bool? MfaEnabled,
    string? Banner,
    int? AccentColor,
    string? Locale,
    bool? Verified,
    string Email,
    int? Flags,
    int? PremiumType,
    int? PublicFlags);
