using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.CreateNotionIntegration;
using Zeus.Api.Application.Integrations.Commands.GenerateNotionOauth2Uri;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.Notion;

[Route("integrations/notion")]
public class NotionCreationController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly ISender _sender;

    public NotionCreationController(ISender sender, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _authUserContext = authUserContext;
    }

    [HttpPost("uri", Name = "GenerateNotionUri")]
    [ProducesResponseType<GenerateNotionUriResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateNotionUri()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var generateUriResult = await _sender.Send(new GenerateNotionOauth2UriCommand(
            authUser.Id));

        return generateUriResult.Match(
            result => Ok(new GenerateNotionUriResponse(result.Uri.ToString())),
            Problem);
    }

    [AllowAnonymous]
    [HttpPost(Name = "CreateNotionIntegration")]
    [ProducesResponseType<CreateNotionIntegrationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNotionIntegration(CreateNotionIntegrationRequest request)
    {
        var createIntegrationResult =
            await _sender.Send(new CreateNotionIntegrationCommand(request.Code, request.State));

        return createIntegrationResult.Match(
            result => CreatedAtRoute(nameof(IntegrationsController.GetIntegrationById),
                new { id = result.IntegrationId }, new CreateNotionIntegrationResponse()),
            Problem);
    }
}
