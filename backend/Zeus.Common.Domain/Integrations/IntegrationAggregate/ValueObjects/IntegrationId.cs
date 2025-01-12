using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class IntegrationId : ValueObject
{
    public IntegrationId(Guid value)
    {
        Value = value;
    }

#pragma warning disable CS8618
    private IntegrationId()
    {
    }
#pragma warning restore CS8618
    public Guid Value { get; }

    public static IntegrationId CreateUnique()
    {
        return new IntegrationId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
