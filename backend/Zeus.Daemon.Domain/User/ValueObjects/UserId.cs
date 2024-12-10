using Zeus.Common.Domain.Models;

namespace Zeus.Daemon.Domain.User.ValueObjects;

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
    
#pragma warning disable CS8618
    private UserId()
    {
    }
#pragma warning restore CS8618
}
