using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.UserAggregate;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public sealed class UserReadRepository: IUserReadRepository
{
    private readonly ZeusDbContext _dbContext;

    public UserReadRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<User> Users => _dbContext.Users.AsNoTracking();

    public async Task<User?> GetUserByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }
}
