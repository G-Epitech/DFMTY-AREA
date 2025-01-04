using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationTriggerId : ValueObject
{
    public Guid Value { get; }

    public AutomationTriggerId(Guid value)
    {
        Value = value;
    }

    public static AutomationTriggerId CreateUnique()
    {
        return new AutomationTriggerId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
