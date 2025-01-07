using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord.ValueObjects;

public class DiscordGuildId : ValueObject
{
    public ulong Value { get; }

    public string ValueString => Value.ToString();

    public DiscordGuildId(ulong value)
    {
        Value = value;
    }

    public DiscordGuildId(string value)
    {
        Value = ulong.Parse(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}