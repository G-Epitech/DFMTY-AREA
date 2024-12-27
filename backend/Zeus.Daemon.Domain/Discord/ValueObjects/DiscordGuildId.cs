using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Discord.ValueObjects;

public sealed class DiscordGuildId : ValueObject
{
    public string Value { get; }

    public DiscordGuildId(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
