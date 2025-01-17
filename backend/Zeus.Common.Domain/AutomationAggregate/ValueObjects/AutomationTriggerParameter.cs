using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Common.Enums;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationTriggerParameter : ValueObject
{
    public required string Value { get; set; }
    public required string Identifier { get; set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Identifier;
    }

    public static bool IsValueValid(string value, VariableType type)
    {
        return type switch
        {
            VariableType.String => IsStringValid(value),
            VariableType.Integer => IsIntegerValid(value),
            VariableType.Boolean => IsBooleanValid(value),
            VariableType.Float => IsFloatValid(value),
            VariableType.Datetime => IsDateTimeValid(value),
            VariableType.Object => IsObjectValid(value),
            _ => false
        };
    }

    private static bool IsStringValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    private static bool IsIntegerValid(string value)
    {
        return int.TryParse(value, out _);
    }

    private static bool IsBooleanValid(string value)
    {
        return bool.TryParse(value, out _);
    }

    private static bool IsFloatValid(string value)
    {
        return float.TryParse(value, out _);
    }

    private static bool IsDateTimeValid(string value)
    {
        return DateTime.TryParse(value, out _);
    }

    private static bool IsObjectValid(string value)
    {
        return true;
    }
}
