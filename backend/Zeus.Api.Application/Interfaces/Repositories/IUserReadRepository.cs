using Zeus.Api.Domain.UserAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IUserReadRepository
{
    public Task<User?> GetUserByIdAsync(UserId userId);
    public Task<User?> GetUserByEmailAsync(string email);
}
