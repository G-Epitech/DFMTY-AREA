using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Errors;
using Zeus.Common.Domain.Authentication.AuthenticationMethodAggregate;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<LoginQueryResult>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAuthenticationMethodReadRepository _authenticationMethodReadRepository;

    public LoginQueryHandler(IUserReadRepository userReadRepository, IJwtGenerator jwtGenerator,
        IAuthenticationMethodReadRepository authenticationMethodReadRepository)
    {
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
        _authenticationMethodReadRepository = authenticationMethodReadRepository;
    }

    public async Task<ErrorOr<LoginQueryResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (await _userReadRepository.GetUserByEmailAsync(query.Email, cancellationToken) is not { } user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (await _authenticationMethodReadRepository.GetAuthenticationMethodsByUserIdAsync(user.Id, cancellationToken)
            is not { } methods)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        var passwordMethod = methods.OfType<PasswordAuthenticationMethod>().FirstOrDefault();
        
        if (passwordMethod is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (!passwordMethod.Password.Verify(query.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return new LoginQueryResult(accessToken, refreshToken);
    }
}
