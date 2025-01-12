using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.UserAggregate.ValueObjects;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class UserId : ValueObject
{
    public UserId(Guid value)
    {
        Value = value;
    }

#pragma warning disable CS8618
    private UserId()
    {
    }
#pragma warning restore CS8618
    public Guid Value { get; }

    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
