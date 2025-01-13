using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;

public sealed class IntegrationLinkRequestId : ValueObject
{
    public IntegrationLinkRequestId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static IntegrationLinkRequestId CreateUnique()
    {
        return new IntegrationLinkRequestId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
