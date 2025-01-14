using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCode;
using Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCredentials;
using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Presentation.Web.Contracts.Authentication;
using Zeus.Api.Presentation.Web.Controllers.Users;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Presentation.Web.Controllers.Authentication.Oauth2;

[AllowAnonymous]
[Route("auth/oauth2/google")]
public class GoogleOAuth2Controller : ApiController
{
    private readonly IMapper _mapper;
    private readonly IOAuth2SettingsProvider _oAuth2SettingsProvider;
    private readonly ISender _sender;

    public GoogleOAuth2Controller(IOAuth2SettingsProvider oAuth2SettingsProvider, ISender sender, IMapper mapper)
    {
        _oAuth2SettingsProvider = oAuth2SettingsProvider;
        _sender = sender;
        _mapper = mapper;
    }


    [HttpGet("configuration", Name = "GoogleOAuth2Configuration")]
    [ProducesResponseType<GoogleOAuth2ConfigurationResponse>(StatusCodes.Status200OK)]
    public Task<IActionResult> GetConfiguration()
    {
        var settings = _oAuth2SettingsProvider.Google;

        var clientIds = new List<GoogleOAuth2ClientIdConfigurationResponse>
        {
            new(GoogleOAuth2ClientIdProvider.Web, settings.Clients.Web.ClientId),
            new(GoogleOAuth2ClientIdProvider.Android, settings.Clients.Android.ClientId),
            new(GoogleOAuth2ClientIdProvider.Ios, settings.Clients.Ios.ClientId)
        };

        var response =
            new GoogleOAuth2ConfigurationResponse(settings.Scopes, clientIds, new Uri(settings.OAuth2Endpoint));

        return Task.FromResult<IActionResult>(Ok(response));
    }

    [HttpPost("from-code", Name = "GoogleOAuth2FromCodeAuthentication")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AuthenticateFromCode(GoogleOAuth2FromCodeRequest fromCodeRequest)
    {
        var command = new GoogleAuthFromCodeCommand(fromCodeRequest.Code);
        var authResult = await _sender.Send(command);

        if (authResult.IsError)
        {
            return Problem(authResult.Errors);
        }

        if (authResult.Value.IsRegistered)
        {
            return CreatedAtRoute(nameof(UserController.GetAuthUser),
                _mapper.Map<AuthenticationResponse>(authResult.Value));
        }

        return Ok(_mapper.Map<AuthenticationResponse>(authResult.Value));
    }

    [HttpPost("from-credentials", Name = "GoogleOAuth2FromCredentialsAuthentication")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AuthenticateFromCredentials(
        GoogleOAuth2FromCredentialsRequest fromCredentialsRequest)
    {
        var command = new GoogleAuthFromCredentialsCommand(new AccessToken(fromCredentialsRequest.AccessToken),
            new RefreshToken(fromCredentialsRequest.RefreshToken), fromCredentialsRequest.TokenType);
        var authResult = await _sender.Send(command);

        if (authResult.IsError)
        {
            return Problem(authResult.Errors);
        }

        if (authResult.Value.IsRegistered)
        {
            return CreatedAtRoute(nameof(UserController.GetAuthUser),
                _mapper.Map<AuthenticationResponse>(authResult.Value));
        }

        return Ok(_mapper.Map<AuthenticationResponse>(authResult.Value));
    }
}
