using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Authentication;
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

    public async Task<ErrorOr<LoginQueryResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (await _userReadRepository.GetUserByEmailAsync(query.Email) is not { } user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return new LoginQueryResult(accessToken, refreshToken);
    }
}
