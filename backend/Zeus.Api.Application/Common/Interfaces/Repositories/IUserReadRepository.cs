using Zeus.Api.Domain.UserAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Common.Interfaces.Repositories;

public interface IUserReadRepository
{
    public User? GetUserById(UserId userId);
    public User? GetUserByEmail(string email);
}
