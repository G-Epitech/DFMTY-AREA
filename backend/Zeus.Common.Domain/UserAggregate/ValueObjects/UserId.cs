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

    public static UserId? TryParse(string? value)
    {
        return Guid.TryParse(value, out var guid) ? new UserId(guid) : null;
    }

    public static UserId Parse(string value)
    {
        return new UserId(Guid.Parse(value));
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
