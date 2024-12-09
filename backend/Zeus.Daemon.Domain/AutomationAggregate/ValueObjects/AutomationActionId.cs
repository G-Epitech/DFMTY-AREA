using Zeus.Common.Domain.Models;

namespace Zeus.Daemon.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationActionId : ValueObject
{
    public Guid Value { get;}

    public AutomationActionId(Guid value)
    {
        Value = value;
    }

    public static AutomationActionId CreateUnique()
    {
        return new AutomationActionId(Guid.NewGuid());
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
