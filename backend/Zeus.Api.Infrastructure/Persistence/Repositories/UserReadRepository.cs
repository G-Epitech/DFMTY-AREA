using Zeus.Api.Application.Common.Interfaces.Repositories;
using Zeus.Api.Domain.UserAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class UserReadRepository: IUserReadRepository
{
    public User? GetUserById(UserId userId)
    {
        return InMemoryUserSet.Users.FirstOrDefault(user => user.Id == userId);
    }

    public User? GetUserByEmail(string email)
    {
        return InMemoryUserSet.Users.FirstOrDefault(user => user.Email == email);
    }
}
