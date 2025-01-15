using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.CreateLeagueOfLegendsIntegration;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.LeagueOfLegends;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.LeagueOfLegends;

[Route("integrations/lol")]
public class LeagueOfLegendsCreationController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly ISender _sender;

    public LeagueOfLegendsCreationController(IAuthUserContext authUserContext, ISender sender)
    {
        _authUserContext = authUserContext;
        _sender = sender;
    }

    [HttpPost(Name = "CreateLeagueOfLegendsIntegration")]
    [ProducesResponseType<CreateLeagueOfLegendsIntegrationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateLeagueOfLegendsIntegration(CreateLeagueOfLegendsIntegrationRequest request)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var createIntegrationResult = await _sender.Send(new CreateLeagueOfLegendsIntegrationCommand(
            authUser.Id, request.GameName, request.TagLine));

        return createIntegrationResult.Match(
            result => CreatedAtRoute(nameof(IntegrationsController.GetIntegrationById),
                new { id = result.IntegrationId }, new CreateLeagueOfLegendsIntegrationResponse()),
            Problem);
    }
}
