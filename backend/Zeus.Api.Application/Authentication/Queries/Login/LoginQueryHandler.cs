using ErrorOr;

using MediatR;

using Zeus.Api.Application.Common.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Authentication;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<LoginQueryResult>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(IUserReadRepository userReadRepository, IJwtGenerator jwtGenerator)
    {
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
    }

    public Task<ErrorOr<LoginQueryResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userReadRepository.GetUserByEmail(query.Email) is not { } user)
        {
            return Task.FromResult<ErrorOr<LoginQueryResult>>(Errors.Authentication.InvalidCredentials);
        }

        if (user.Password != query.Password)
        {
            return Task.FromResult<ErrorOr<LoginQueryResult>>(Errors.Authentication.InvalidCredentials);
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return Task.FromResult<ErrorOr<LoginQueryResult>>(new LoginQueryResult(accessToken, refreshToken));
    }
}
