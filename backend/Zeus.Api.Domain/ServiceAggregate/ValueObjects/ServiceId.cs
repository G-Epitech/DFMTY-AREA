using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.ServiceAggregate.ValueObjects;

public sealed class ServiceId : ValueObject
{
    public Guid Value { get; }
    
    public ServiceId(Guid value)
    {
        Value = value;
    }

    public static ServiceId CreateUnique()
    {
        return new ServiceId(Guid.NewGuid());
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
