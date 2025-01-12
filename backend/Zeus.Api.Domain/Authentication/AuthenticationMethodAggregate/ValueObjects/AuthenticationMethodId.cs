using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class AuthenticationMethodId : ValueObject
{
    public AuthenticationMethodId(Guid value)
    {
        Value = value;
    }

#pragma warning disable CS8618
    private AuthenticationMethodId()
    {
    }
#pragma warning restore CS8618
    public Guid Value { get; }

    public static AuthenticationMethodId CreateUnique()
    {
        return new AuthenticationMethodId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
