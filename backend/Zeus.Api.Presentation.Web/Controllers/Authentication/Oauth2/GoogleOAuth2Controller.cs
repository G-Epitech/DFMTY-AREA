using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Interfaces.Services.Settings.OAuth2;
using Zeus.Api.Presentation.Web.Contracts.Authentication;

namespace Zeus.Api.Presentation.Web.Controllers.Authentication.Oauth2;

[AllowAnonymous]
[Route("auth/oauth2/google")]
public class GoogleOAuth2Controller : ApiController
{
    private readonly IOAuth2SettingsProvider _oAuth2SettingsProvider;

    public GoogleOAuth2Controller(IOAuth2SettingsProvider oAuth2SettingsProvider)
    {
        _oAuth2SettingsProvider = oAuth2SettingsProvider;
    }


    [HttpGet("configuration", Name = "GoogleOAuth2Configuration")]
    [ProducesResponseType<GoogleOAuth2ConfigurationResponse>(StatusCodes.Status200OK)]
    public Task<IActionResult> GetConfiguration()
    {
        var settings = _oAuth2SettingsProvider.Google;
        var response =
            new GoogleOAuth2ConfigurationResponse(settings.Scopes, settings.ClientId, new Uri(settings.OAuth2Endpoint));

        return Task.FromResult<IActionResult>(Ok(response));
    }
}
