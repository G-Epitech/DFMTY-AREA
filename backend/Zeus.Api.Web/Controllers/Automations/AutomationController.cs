using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Automations.Commands.CreateAutomation;
using Zeus.Api.Application.Automations.Query.GetAutomation;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Web.Contracts.Automations;

namespace Zeus.Api.Web.Controllers.Automations;

[Route("automations")]
public class AutomationController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IAuthUserContext _authUserContext;

    public AutomationController(ISender sender, IAuthUserContext authUserContext, IMapper mapper)
    {
        _sender = sender;
        _authUserContext = authUserContext;
        _mapper = mapper;
    }

    [HttpPost(Name = "CreateAutomation")]
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
            result => CreatedAtRoute(nameof(GetAutomation), new { id = result.Id.Value }, null),
            Problem);
    }

    [HttpGet("{id:guid}", Name = "GetAutomation")]
    [ProducesResponseType<GetAutomationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutomation(Guid id)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var query = new GetAutomationQuery(authUser.Id, id);

        var automation = await _sender.Send(query);
        if (automation.IsError)
        {
            return Problem(automation.Errors);
        }

        var automationResponse = _mapper.Map<GetAutomationResponse>(automation.Value);

        return Ok(automationResponse);
    }
}
