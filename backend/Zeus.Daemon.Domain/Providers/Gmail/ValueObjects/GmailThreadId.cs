using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.Gmail.ValueObjects;

public class GmailThreadId : ValueObject
{
    public GmailThreadId(string value)
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
