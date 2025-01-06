using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IUserWriteRepository
{
    public Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    public Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    public Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
}
