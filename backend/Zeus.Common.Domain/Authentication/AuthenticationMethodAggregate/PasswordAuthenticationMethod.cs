using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class PasswordAuthenticationMethod : AuthenticationMethod
{
    public Password Password { get; }

    private PasswordAuthenticationMethod(
        AuthenticationMethodId id,
        UserId userId,
        Password password,
        DateTime updatedAt,
        DateTime createdAt) : base(id, userId, AuthenticationMethodType.Password, updatedAt, createdAt)
    {
        Password = password;
    }

#pragma warning disable CS8618
    private PasswordAuthenticationMethod() { }
#pragma warning restore CS8618
}
