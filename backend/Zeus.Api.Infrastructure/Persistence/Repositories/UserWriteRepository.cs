using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class UserWriteRepository : IUserWriteRepository
{
    public Task AddUserAsync(User user)
    {
        InMemoryStore.Users.Add(user);

        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user)
    {
        InMemoryStore.Users.Remove(user);
        InMemoryStore.Users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(User user)
    {
        InMemoryStore.Users.Remove(user);
        return Task.CompletedTask;
    }
}
