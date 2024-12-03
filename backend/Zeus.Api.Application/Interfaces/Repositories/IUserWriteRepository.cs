using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IUserWriteRepository
{
    public Task AddUserAsync(User user);
    public Task UpdateUserAsync(User user);
    public Task DeleteUserAsync(User user);
}
