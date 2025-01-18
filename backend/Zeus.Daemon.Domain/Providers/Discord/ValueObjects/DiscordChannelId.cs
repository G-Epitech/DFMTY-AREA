using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.Discord.ValueObjects;

public sealed class DiscordChannelId : ValueObject
{
    public DiscordChannelId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
