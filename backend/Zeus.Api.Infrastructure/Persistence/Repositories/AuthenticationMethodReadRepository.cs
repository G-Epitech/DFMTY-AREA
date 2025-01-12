using Microsoft.EntityFrameworkCore;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

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
    
    public async Task<PasswordAuthenticationMethod?> GetPasswordAuthenticationMethodAsync(UserId userId,
        CancellationToken cancellationToken = default)
    {
        return await AuthenticationMethods.OfType<PasswordAuthenticationMethod>().FirstOrDefaultAsync(
            authenticationMethod => authenticationMethod.UserId == userId,
            cancellationToken: cancellationToken);
    }

    public async Task<GoogleAuthenticationMethod?> GetGoogleAuthenticationMethodAsync(string googleId,
        CancellationToken cancellationToken = default)
    {
        return await AuthenticationMethods.OfType<GoogleAuthenticationMethod>().FirstOrDefaultAsync(
            authenticationMethod => authenticationMethod.ProviderUserId == googleId,
            cancellationToken: cancellationToken);
    }
}
