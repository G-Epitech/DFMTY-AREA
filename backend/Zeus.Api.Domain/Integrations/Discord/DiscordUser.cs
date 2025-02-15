using Zeus.Api.Domain.Integrations.Discord.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord;

public class DiscordUser : Entity<DiscordUserId>
{
    private DiscordUser(
        DiscordUserId id,
        string username,
        string email,
        string displayName,
        Uri avatarUri,
        string[] flags) : base(id)
    {
        Username = username;
        Email = email;
        DisplayName = displayName;
        AvatarUri = avatarUri;
        Flags = flags;
    }

    public string Username { get; private set; }
    public string Email { get; private set; }
    public string DisplayName { get; private set; }
    public Uri AvatarUri { get; private set; }
    public string[] Flags { get; private set; }

    public static DiscordUser Create(
        DiscordUserId id,
        string username,
        string email,
        string displayName,
        Uri avatarUri,
        string[] flags)
    {
        return new DiscordUser(id, username, email, displayName, avatarUri, flags);
    }
}
