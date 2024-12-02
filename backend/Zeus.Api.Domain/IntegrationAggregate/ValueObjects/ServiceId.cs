using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.IntegrationAggregate.ValueObjects;

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
