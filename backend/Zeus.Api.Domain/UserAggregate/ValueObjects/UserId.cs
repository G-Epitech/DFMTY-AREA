using Zeus.Api.Domain.Common.Models;

namespace Zeus.Api.Domain.UserAggregate.ValueObjects;

public sealed class UserId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
