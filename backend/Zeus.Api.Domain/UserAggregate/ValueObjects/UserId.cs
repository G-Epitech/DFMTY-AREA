using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.UserAggregate.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public UserId(Guid value)
    {
        Value = value;
    }
    
    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
