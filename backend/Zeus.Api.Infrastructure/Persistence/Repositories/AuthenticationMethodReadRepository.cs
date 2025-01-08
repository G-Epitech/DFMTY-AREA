using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public class AuthenticationMethodReadRepository : IAuthenticationMethodReadRepository
{
    private readonly ZeusDbContext _dbContext;

    public AuthenticationMethodReadRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IAsyncQueryable<AuthenticationMethod> AuthenticationMethods => _dbContext.AuthenticationMethods
        .AsNoTracking()
        .AsAsyncEnumerable()
        .AsAsyncQueryable();

    public async Task<AuthenticationMethod?> GetAuthenticationMethodByIdAsync(AuthenticationMethodId id,
        CancellationToken cancellationToken = default)
    {
        return await AuthenticationMethods.FirstOrDefaultAsync(authenticationMethod => authenticationMethod.Id == id,
            cancellationToken: cancellationToken);
    }

    public async Task<List<AuthenticationMethod>> GetAuthenticationMethodsByUserIdAsync(UserId userId,
        CancellationToken cancellationToken = default)
    {
        return await AuthenticationMethods
            .Where(authenticationMethod => authenticationMethod.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}
