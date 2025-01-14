using System.Diagnostics.CodeAnalysis;

using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class PasswordAuthenticationMethod : AuthenticationMethod
{
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
    public Password Password { get; }

    public static PasswordAuthenticationMethod Create(UserId userId, Password password)
    {
        return new PasswordAuthenticationMethod(
            AuthenticationMethodId.CreateUnique(),
            userId,
            password,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
