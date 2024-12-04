using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;

public sealed class IntegrationId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

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
