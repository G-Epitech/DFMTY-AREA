using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class AutomationId : ValueObject
{
    public AutomationId(Guid value)
    {
        Value = value;
    }

#pragma warning disable CS8618
    private AutomationId()
    {
    }
#pragma warning restore CS8618
    public Guid Value { get; }

    public static AutomationId CreateUnique()
    {
        return new AutomationId(Guid.NewGuid());
    }

    public static AutomationId? TryParse(string? value)
    {
        return Guid.TryParse(value, out var guid) ? new AutomationId(guid) : null;
    }

    public static AutomationId Parse(string value)
    {
        return new AutomationId(Guid.Parse(value));
    }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
