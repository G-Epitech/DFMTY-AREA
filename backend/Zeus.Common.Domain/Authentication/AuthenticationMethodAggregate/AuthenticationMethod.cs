using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;

public abstract class AuthenticationMethod : AggregateRoot<AuthenticationMethodId>
{
    public UserId UserId { get; }
    public AuthenticationMethodType Type { get; }

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
}
