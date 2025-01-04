using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

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

#pragma warning disable CS8618
    private IntegrationId()
    {
    }
#pragma warning restore CS8618
}
