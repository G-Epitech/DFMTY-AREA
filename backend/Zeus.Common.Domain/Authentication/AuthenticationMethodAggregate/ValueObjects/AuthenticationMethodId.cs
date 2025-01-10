using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class AuthenticationMethodId : ValueObject
{
    public Guid Value { get; }

    public AuthenticationMethodId(Guid value)
    {
        Value = value;
    }

    public static AuthenticationMethodId CreateUnique()
    {
        return new AuthenticationMethodId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private AuthenticationMethodId()
    {
    }
#pragma warning restore CS8618
}
