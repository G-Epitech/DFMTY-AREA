using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Errors;

namespace Zeus.Api.Application.Authentication.Queries.PasswordLogin;

public class
    PasswordAuthLoginQueryHandler : IRequestHandler<PasswordAuthLoginQuery, ErrorOr<PasswordAuthLoginQueryResult>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAuthenticationMethodReadRepository _authenticationMethodReadRepository;

    public PasswordAuthLoginQueryHandler(IUserReadRepository userReadRepository, IJwtGenerator jwtGenerator,
        IAuthenticationMethodReadRepository authenticationMethodReadRepository)
    {
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
        _authenticationMethodReadRepository = authenticationMethodReadRepository;
    }

    public async Task<ErrorOr<PasswordAuthLoginQueryResult>> Handle(PasswordAuthLoginQuery query,
        CancellationToken cancellationToken)
    {
        if (await _userReadRepository.GetUserByEmailAsync(query.Email, cancellationToken) is not { } user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (await _authenticationMethodReadRepository.GetPasswordAuthenticationMethodAsync(user.Id, cancellationToken)
            is not { } passwordMethod)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (!passwordMethod.Password.Verify(query.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return new PasswordAuthLoginQueryResult(accessToken, refreshToken);
    }
}
