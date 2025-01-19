using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.CreateGithubIntegration;
using Zeus.Api.Application.Integrations.Commands.GenerateGithubOauth2Uri;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Github;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.Github;

[Route("integrations/github")]
public class GithubCreationController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly ISender _sender;

    public GithubCreationController(ISender sender, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _authUserContext = authUserContext;
    }

    [HttpPost("uri", Name = "GenerateGithubUri")]
    [ProducesResponseType<GenerateGithubUriResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateGithubUri()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var generateUriResult = await _sender.Send(new GenerateGithubOauth2UriCommand(
            authUser.Id));

        return generateUriResult.Match(
            result => Ok(new GenerateGithubUriResponse(result.Uri.ToString())),
            Problem);
    }

    [AllowAnonymous]
    [HttpPost(Name = "CreateGithubIntegration")]
    [ProducesResponseType<CreateGithubIntegrationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateGithubIntegration(CreateGithubIntegrationRequest request)
    {
        var createIntegrationResult =
            await _sender.Send(new CreateGithubIntegrationCommand(request.Code, request.State));

        return createIntegrationResult.Match(
            result => CreatedAtRoute(nameof(IntegrationsController.GetIntegrationById),
                new { id = result.IntegrationId }, new CreateGithubIntegrationResponse()),
            Problem);
    }
}
