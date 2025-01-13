using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.CreateOpenAiIntegration;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.OpenAi;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.OpenAi;

[Route("integrations/openai")]
public class OpenAiCreationController : ApiController
{
    private readonly ISender _sender;
    private readonly IAuthUserContext _authUserContext;

    public OpenAiCreationController(ISender sender, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _authUserContext = authUserContext;
    }

    [HttpPost(Name = "CreateOpenAiIntegration")]
    [ProducesResponseType<CreateOpenAiIntegrationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOpenAiIntegration(CreateOpenAiIntegrationRequest request)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var createIntegrationResult =
            await _sender.Send(
                new CreateOpenAiIntegrationCommand(authUser.Id, request.ApiToken, request.AdminApiToken));

        return createIntegrationResult.Match(
            result => CreatedAtRoute(nameof(IntegrationsController.GetIntegrationById),
                new { id = result.IntegrationId }, new CreateOpenAiIntegrationResponse()),
            Problem);
    }
}
