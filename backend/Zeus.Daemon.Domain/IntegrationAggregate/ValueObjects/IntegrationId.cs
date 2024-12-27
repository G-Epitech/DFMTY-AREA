using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;

public sealed class IntegrationId : ValueObject
{
    public Guid Value { get; }

    public IntegrationId(Guid value)
    {
        Value = value;
    }

    public static IntegrationId CreateUnique()
    {
        return new IntegrationId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
