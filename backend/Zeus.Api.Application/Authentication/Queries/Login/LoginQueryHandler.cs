using ErrorOr;

using MediatR;

using Zeus.Api.Application.Authentication.Common;
using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Authentication;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(IUserReadRepository userReadRepository, IJwtGenerator jwtGenerator)
    {
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
    }

    public Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userReadRepository.GetUserByEmail(query.Email) is not { } user)
        {
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.Authentication.InvalidCredentials);
        }

        if (user.Password != query.Password)
        {
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.Authentication.InvalidCredentials);
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return Task.FromResult<ErrorOr<AuthenticationResult>>(new AuthenticationResult(accessToken, refreshToken));
    }
}
