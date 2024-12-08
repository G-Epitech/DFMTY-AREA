using Zeus.Api.Domain.UserAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IUserReadRepository
{
    public Task<User?> GetUserByIdAsync(UserId userId,
        CancellationToken cancellationToken = default);
    public Task<User?> GetUserByEmailAsync(string email,
        CancellationToken cancellationToken = default);
}
