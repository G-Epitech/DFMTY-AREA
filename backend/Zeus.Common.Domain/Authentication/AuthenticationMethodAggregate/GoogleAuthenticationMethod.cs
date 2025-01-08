using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class GoogleAuthenticationMethod : AuthenticationMethod
{
    public AccessToken AccessToken { get; }
    public RefreshToken RefreshToken { get; }

    private GoogleAuthenticationMethod(
        AuthenticationMethodId id,
        UserId userId,
        AccessToken accessToken,
        RefreshToken refreshToken,
        DateTime updatedAt,
        DateTime createdAt) : base(id, userId, AuthenticationMethodType.Google, updatedAt, createdAt)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

#pragma warning disable CS8618
    private GoogleAuthenticationMethod() { }
#pragma warning restore CS8618
}
