using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class UserWriteRepository : IUserWriteRepository
{
    public void AddUser(User user)
    {
        InMemoryUserSet.Users.Add(user);
    }

    public void UpdateUser(User user)
    {
        InMemoryUserSet.Users.Remove(user);
        InMemoryUserSet.Users.Add(user);
    }

    public void DeleteUser(User user)
    {
        InMemoryUserSet.Users.Remove(user);
    }
}
