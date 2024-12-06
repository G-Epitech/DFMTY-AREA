using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.UserAggregate;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class UserReadRepository: IUserReadRepository
{
    private readonly ZeusDbContext _dbContext;

    public UserReadRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<User> Users => _dbContext.Users.AsNoTracking();

    public async Task<User?> GetUserByIdAsync(UserId userId)
    {
        return await Users.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Users.FirstOrDefaultAsync(user => user.Email == email);
    }
}
