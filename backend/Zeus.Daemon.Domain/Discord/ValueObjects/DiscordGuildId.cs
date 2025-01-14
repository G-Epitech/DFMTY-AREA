using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Discord.ValueObjects;

public sealed class DiscordGuildId : ValueObject
{
    public DiscordGuildId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
