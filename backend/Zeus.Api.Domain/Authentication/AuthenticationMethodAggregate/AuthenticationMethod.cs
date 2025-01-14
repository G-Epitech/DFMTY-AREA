using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;

public abstract class AuthenticationMethod : AggregateRoot<AuthenticationMethodId>
{
    protected AuthenticationMethod(
        AuthenticationMethodId id,
        UserId userId,
        AuthenticationMethodType type,
        DateTime updatedAt,
        DateTime createdAt) : base(id, updatedAt, createdAt)
    {
        UserId = userId;
        Type = type;
    }

#pragma warning disable CS8618
    protected AuthenticationMethod() { }
#pragma warning restore CS8618
    public UserId UserId { get; }
    public AuthenticationMethodType Type { get; }
}
