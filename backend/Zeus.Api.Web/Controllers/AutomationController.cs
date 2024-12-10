using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Automations.Commands.CreateAutomation;
using Zeus.Api.Infrastructure.Authentication.Context;

namespace Zeus.Api.Web.Controllers;

[Route("automations")]
public class AutomationController : ApiController
{
    private readonly ISender _sender;
    private readonly IAuthUserContext _authUserContext;

    public AutomationController(ISender sender, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _authUserContext = authUserContext;
    }

    [HttpPost("", Name = "CreateAutomation")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAutomation()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var command = new CreateAutomationCommand(authUser.Id);
        var automation = await _sender.Send(command);

        return automation.Match(
            result => CreatedAtRoute("", new { id = result.Id.Value }, null),
            Problem);
    }
}
