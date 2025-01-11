using ErrorOr;

using MediatR;

using Zeus.Api.Application.Authentication.Commands.GoogleRegister;
using Zeus.Api.Application.Interfaces.Authentication;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.OAuth2;
using Zeus.Api.Domain.Errors;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCredentials;

public class GoogleAuthFromCredentialsCommandHandler : IRequestHandler<GoogleAuthFromCredentialsCommand,
    ErrorOr<GoogleAuthFromCredentialsCommandResult>>
{
    private readonly IGoogleOAuth2Service _googleOAuth2Service;
    private readonly IAuthenticationMethodReadRepository _authenticationMethodReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly ISender _sender;

    public GoogleAuthFromCredentialsCommandHandler(IGoogleOAuth2Service googleOAuth2Service,
        IAuthenticationMethodReadRepository authenticationMethodReadRepository, IUserReadRepository userReadRepository,
        IJwtGenerator jwtGenerator, ISender sender)
    {
        _googleOAuth2Service = googleOAuth2Service;
        _authenticationMethodReadRepository = authenticationMethodReadRepository;
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
        _sender = sender;
    }

    public async Task<ErrorOr<GoogleAuthFromCredentialsCommandResult>> Handle(GoogleAuthFromCredentialsCommand command,
        CancellationToken cancellationToken)
    {
        var googleUser = await _googleOAuth2Service.GetUserAsync(command.AccessToken);
        if (googleUser.IsError)
        {
            return googleUser.Errors;
        }

        var authenticationMethod =
            await _authenticationMethodReadRepository.GetGoogleAuthenticationMethodAsync(googleUser.Value.Id.Value,
                cancellationToken);

        if (authenticationMethod is null)
        {
            var registrationResult = await _sender.Send(new GoogleAuthRegisterCommand(command.AccessToken,
                command.RefreshToken, googleUser.Value), cancellationToken);

            if (registrationResult.IsError)
            {
                return registrationResult.Errors;
            }

            return new GoogleAuthFromCredentialsCommandResult(true, registrationResult.Value.AccessToken,
                registrationResult.Value.RefreshToken,
                registrationResult.Value.UserId);
        }

        var user = await _userReadRepository.GetUserByEmailAsync(googleUser.Value.Email, cancellationToken);
        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var accessToken = _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken(user);

        return new GoogleAuthFromCredentialsCommandResult(false, accessToken, refreshToken, user.Id.Value);
    }
}
