using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public class AuthenticationMethodWriteRepository : IAuthenticationMethodWriteRepository
{
    private readonly ZeusDbContext _dbContext;

    public AuthenticationMethodWriteRepository(ZeusDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAuthenticationMethodAsync(AuthenticationMethod authenticationMethod,
        CancellationToken cancellationToken = default)
    {
        _dbContext.AuthenticationMethods.Add(authenticationMethod);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAuthenticationMethodAsync(AuthenticationMethod authenticationMethod,
        CancellationToken cancellationToken = default)
    {
        _dbContext.AuthenticationMethods.Update(authenticationMethod);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAuthenticationMethodAsync(AuthenticationMethod authenticationMethod,
        CancellationToken cancellationToken = default)
    {
        _dbContext.AuthenticationMethods.Remove(authenticationMethod);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
