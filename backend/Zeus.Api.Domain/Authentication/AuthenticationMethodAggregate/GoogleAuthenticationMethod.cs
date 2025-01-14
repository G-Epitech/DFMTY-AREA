using System.Diagnostics.CodeAnalysis;

using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class GoogleAuthenticationMethod : AuthenticationMethod
{
    private GoogleAuthenticationMethod(
        AuthenticationMethodId id,
        UserId userId,
        AccessToken accessToken,
        RefreshToken refreshToken,
        string providerUserId,
        DateTime updatedAt,
        DateTime createdAt) : base(id, userId, AuthenticationMethodType.Google, updatedAt, createdAt)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ProviderUserId = providerUserId;
    }

#pragma warning disable CS8618
    private GoogleAuthenticationMethod() { }
#pragma warning restore CS8618
    public AccessToken AccessToken { get; }
    public RefreshToken RefreshToken { get; }
    public string ProviderUserId { get; }

    public static GoogleAuthenticationMethod Create(UserId userId, AccessToken accessToken, RefreshToken refreshToken, string providerUserId)
    {
        return new GoogleAuthenticationMethod(
            AuthenticationMethodId.CreateUnique(),
            userId,
            accessToken,
            refreshToken,
            providerUserId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
