using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationTriggerId : ValueObject
{
    public AutomationTriggerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AutomationTriggerId CreateUnique()
    {
        return new AutomationTriggerId(Guid.NewGuid());
    }

    public static AutomationTriggerId? TryParse(string? value)
    {
        return Guid.TryParse(value, out var guid) ? new AutomationTriggerId(guid) : null;
    }

    public static AutomationTriggerId Parse(string value)
    {
        return new AutomationTriggerId(Guid.Parse(value));
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
