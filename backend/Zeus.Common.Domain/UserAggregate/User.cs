using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.UserAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class User : AggregateRoot<UserId>
{
    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, updatedAt, createdAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    public static User Create(
        string firstName,
        string lastName,
        string email)
    {
        return new User(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
