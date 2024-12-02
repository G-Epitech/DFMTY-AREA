using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.UserAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class UserReadRepository: IUserReadRepository
{
    public Task<User?> GetUserByIdAsync(UserId userId)
    {
        return Task.FromResult(InMemoryStore.Users.FirstOrDefault(user => user.Id == userId));
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return Task.FromResult(InMemoryStore.Users.FirstOrDefault(user => user.Email == email));
    }
}
