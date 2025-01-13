using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord.ValueObjects;

public class DiscordChannelId : ValueObject
{
    public DiscordChannelId(ulong value)
    {
        Value = value;
    }

    public DiscordChannelId(string value)
    {
        Value = ulong.Parse(value);
    }

    public ulong Value { get; }

    public string ValueString => Value.ToString();

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
