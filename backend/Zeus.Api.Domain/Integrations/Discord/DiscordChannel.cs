using Zeus.Api.Domain.Integrations.Discord.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord;

public class DiscordChannel : Entity<DiscordChannelId>
{
    private DiscordChannel(
        DiscordChannelId id,
        string name,
        DiscordChannelType type) : base(id)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; private set; }
    public DiscordChannelType Type { get; private set; }

    public static DiscordChannel Create(
        DiscordChannelId id,
        string name,
        DiscordChannelType type)
    {
        return new DiscordChannel(id, name, type);
    }
}
