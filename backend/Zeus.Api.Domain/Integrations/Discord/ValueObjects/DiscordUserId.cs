using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Discord.ValueObjects;

public class DiscordUserId : ValueObject
{
    public ulong Value { get; }

    public string ValueString => Value.ToString();

    public DiscordUserId(ulong value)
    {
        Value = value;
    }

    public DiscordUserId(string value)
    {
        Value = ulong.Parse(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
