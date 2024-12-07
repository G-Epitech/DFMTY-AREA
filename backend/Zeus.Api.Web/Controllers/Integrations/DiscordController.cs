using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.CreateDiscordIntegration;
using Zeus.Api.Application.Integrations.Commands.GenerateDiscordOauth2Uri;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Web.Contracts.Integrations.Discord;

namespace Zeus.Api.Web.Controllers.Integrations;

[Route("integrations/discord")]
public class DiscordController : ApiController
{
    private readonly ISender _sender;
    private readonly IAuthUserContext _authUserContext;

    public DiscordController(ISender sender, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _authUserContext = authUserContext;
    }

    [HttpPost("uri", Name = "GenerateDiscordUri")]
    [ProducesResponseType<GenerateDiscordUriResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateDiscordUri()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var generateUriResult = await _sender.Send(new GenerateDiscordOauth2UriCommand(
            authUser.Id));

        return generateUriResult.Match(
            result => Ok(new GenerateDiscordUriResponse(result.Uri.ToString())),
            Problem);
    }

    [AllowAnonymous]
    [HttpPost(Name = "CreateDiscordIntegration")]
    [ProducesResponseType<CreateDiscordIntegrationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateDiscordIntegration(CreateDiscordIntegrationRequest request)
    {
        var createIntegrationResult =
            await _sender.Send(new CreateDiscordIntegrationCommand(request.Code, request.State));

        return createIntegrationResult.Match(
            _ => Ok(new CreateDiscordIntegrationResponse()),
            Problem);
    }
}
