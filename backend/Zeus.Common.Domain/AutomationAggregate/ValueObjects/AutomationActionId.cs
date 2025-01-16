using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationActionId : ValueObject
{
    public AutomationActionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AutomationActionId CreateUnique()
    {
        return new AutomationActionId(Guid.NewGuid());
    }

    public static AutomationActionId? TryParse(string? value)
    {
        return Guid.TryParse(value, out var guid) ? new AutomationActionId(guid) : null;
    }

    public static AutomationActionId Parse(string value)
    {
        return new AutomationActionId(Guid.Parse(value));
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
