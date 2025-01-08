namespace Zeus.Api.Domain.Integrations.Discord.ValueObjects;

public enum DiscordChannelType
{
    GuildText = 0,
    DirectMessage = 1,
    GuildVoice = 2,
    GroupDirectMessage = 3,
    GuildCategory = 4,
    GuildNews = 5,
    AnnouncementThread = 10,
    PublicThread = 11,
    PrivateThread = 12,
    StageVoice = 13,
    GuildDirectory = 14,
    GuildForum = 15,
    GuildMedia = 16,
}
