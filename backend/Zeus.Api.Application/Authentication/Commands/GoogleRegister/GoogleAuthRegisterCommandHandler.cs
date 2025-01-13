using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Application.Authentication.Commands.GoogleRegister;

public class
    GoogleAuthRegisterCommandHandler : IRequestHandler<GoogleAuthRegisterCommand,
    ErrorOr<GoogleAuthRegisterCommandResult>>
{
    private readonly IAuthenticationMethodWriteRepository _authenticationMethodWriteRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserWriteRepository _userWriteRepository;

    public GoogleAuthRegisterCommandHandler(IJwtGenerator jwtGenerator, IUserWriteRepository userWriteRepository,
        IAuthenticationMethodWriteRepository authenticationMethodWriteRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userWriteRepository = userWriteRepository;
        _authenticationMethodWriteRepository = authenticationMethodWriteRepository;
    }

    public async Task<ErrorOr<GoogleAuthRegisterCommandResult>> Handle(GoogleAuthRegisterCommand command,
        CancellationToken cancellationToken)
    {
        var googleUser = command.GoogleUser;
        var user = User.Create(googleUser.GivenName, googleUser.FamilyName, googleUser.Email);
        var googleAuthMethod =
            GoogleAuthenticationMethod.Create(user.Id, command.AccessToken, command.RefreshToken, googleUser.Id.Value);

        await _userWriteRepository.AddUserAsync(user, cancellationToken);

        try
        {
            await _authenticationMethodWriteRepository.AddAuthenticationMethodAsync(googleAuthMethod,
                cancellationToken);
        }
        catch (Exception)
        {
            await _userWriteRepository.DeleteUserAsync(user, cancellationToken);
            throw;
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return new GoogleAuthRegisterCommandResult(accessToken, refreshToken, user.Id.Value);
    }
}
