using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;

public sealed class AutomationTriggerParameter : ValueObject
{
    public required string Value { get; set; }
    public required string Identifier { get; set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Identifier;
    }
}
