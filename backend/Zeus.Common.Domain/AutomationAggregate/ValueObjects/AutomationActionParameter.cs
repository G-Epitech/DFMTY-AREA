using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.AutomationAggregate.Enums;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationActionParameter : ValueObject
{
    public required AutomationActionParameterType Type { get; set; }
    public required string Value { get; set; }
    public required string Identifier { get; set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
        yield return Value;
        yield return Identifier;
    }
}
