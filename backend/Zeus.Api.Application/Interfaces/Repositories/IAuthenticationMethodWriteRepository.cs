using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IAuthenticationMethodWriteRepository
{
    public Task AddAuthenticationMethodAsync(AuthenticationMethod authenticationMethod,
        CancellationToken cancellationToken = default);

    public Task UpdateAuthenticationMethodAsync(AuthenticationMethod authenticationMethod,
        CancellationToken cancellationToken = default);

    public Task DeleteAuthenticationMethodAsync(AuthenticationMethod authenticationMethod,
        CancellationToken cancellationToken = default);
}
