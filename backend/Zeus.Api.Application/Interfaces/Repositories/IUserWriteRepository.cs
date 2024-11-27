using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IUserWriteRepository
{
    public void AddUser(User user);
    public void UpdateUser(User user);
    public void DeleteUser(User user);
}
