using System.Diagnostics.CodeAnalysis;

using Zeus.Api.Domain.AutomationAggregate.Enums;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.AutomationAggregate.ValueObjects;

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
