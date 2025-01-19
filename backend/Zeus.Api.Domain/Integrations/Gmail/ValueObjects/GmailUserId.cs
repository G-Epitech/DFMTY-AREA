using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Gmail.ValueObjects;

public class GmailUserId : ValueObject
{
    public GmailUserId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
