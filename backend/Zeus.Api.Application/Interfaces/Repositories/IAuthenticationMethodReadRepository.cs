using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IAuthenticationMethodReadRepository
{
    public Task<AuthenticationMethod?> GetAuthenticationMethodByIdAsync(AuthenticationMethodId id,
        CancellationToken cancellationToken = default);

    public Task<List<AuthenticationMethod>> GetAuthenticationMethodsByUserIdAsync(UserId userId,
        CancellationToken cancellationToken = default);

    public Task<PasswordAuthenticationMethod?> GetPasswordAuthenticationMethodAsync(UserId userId,
        CancellationToken cancellationToken = default);

    public Task<GoogleAuthenticationMethod?> GetGoogleAuthenticationMethodAsync(string googleId,
        CancellationToken cancellationToken = default);
}
