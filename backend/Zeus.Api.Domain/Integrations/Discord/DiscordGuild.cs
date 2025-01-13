using Zeus.Api.Domain.Integrations.Discord.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord;

public class DiscordGuild : Entity<DiscordGuildId>
{
    private DiscordGuild(
        DiscordGuildId id,
        string name,
        Uri iconUri,
        uint approximateMemberCount) : base(id)
    {
        Name = name;
        IconUri = iconUri;
        ApproximateMemberCount = approximateMemberCount;
    }

    public string Name { get; private set; }
    public Uri IconUri { get; private set; }
    public uint ApproximateMemberCount { get; private set; }

    public static DiscordGuild Create(
        DiscordGuildId id,
        string name,
        Uri iconUri,
        uint approximateMemberCount)
    {
        return new DiscordGuild(id, name, iconUri, approximateMemberCount);
    }
}
