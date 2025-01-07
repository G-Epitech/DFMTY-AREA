using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.CreateDiscordIntegration;
using Zeus.Api.Application.Integrations.Commands.GenerateDiscordOauth2Uri;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.Discord;

[Route("integrations/discord")]
public class DiscordCreationController : ApiController
{
    private readonly ISender _sender;
    private readonly IAuthUserContext _authUserContext;

    public DiscordCreationController(ISender sender, IAuthUserContext authUserContext)
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
    [ProducesResponseType<CreateDiscordIntegrationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDiscordIntegration(CreateDiscordIntegrationRequest request)
    {
        var createIntegrationResult =
            await _sender.Send(new CreateDiscordIntegrationCommand(request.Code, request.State));

        return createIntegrationResult.Match(
            result => CreatedAtRoute(nameof(IntegrationsController.GetIntegrationById),
                new { id = result.IntegrationId }, new CreateDiscordIntegrationResponse()),
            Problem);
    }
}
