using Zeus.Common.Domain.Models;

namespace Zeus.Daemon.Domain.Discord.ValueObjects;

public sealed class DiscordChannelId : ValueObject
{
    public string Value { get; }

    public DiscordChannelId(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
