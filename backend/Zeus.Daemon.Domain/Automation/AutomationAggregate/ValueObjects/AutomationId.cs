using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;

public sealed class AutomationId : ValueObject
{
    public Guid Value { get; }

    public AutomationId(Guid value)
    {
        Value = value;
    }

    public static AutomationId CreateUnique()
    {
        return new AutomationId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
