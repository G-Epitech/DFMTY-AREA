using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;

public sealed class IntegrationLinkRequestId : ValueObject
{
    public Guid Value { get; }

    public IntegrationLinkRequestId(Guid value)
    {
        Value = value;
    }

    public static IntegrationLinkRequestId CreateUnique()
    {
        return new IntegrationLinkRequestId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
