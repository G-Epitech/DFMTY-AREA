using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.Gmail;

public sealed class GmailWatchingId : ValueObject
{
    public GmailWatchingId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static GmailWatchingId CreateUnique()
    {
        return new GmailWatchingId(Guid.NewGuid());
    }
}
