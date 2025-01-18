using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Automations.Commands.CreateAutomation;
using Zeus.Api.Application.Automations.Query.GetAutomation;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Automations;
using Zeus.Common.Domain.ProvidersSettings;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Presentation.Web.Controllers.Automations;

[Route("automations")]
public class AutomationController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly IMapper _mapper;
    private readonly ProvidersSettings _providersSettings;
    private readonly ISender _sender;

    public AutomationController(ISender sender, IAuthUserContext authUserContext, IMapper mapper,
        ProvidersSettings providersSettings)
    {
        _sender = sender;
        _authUserContext = authUserContext;
        _mapper = mapper;
        _providersSettings = providersSettings;
    }

    [HttpPost(Name = "CreateAutomation")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateAutomation(CreateAutomationRequest request)
    {
        var userId = _authUserContext.User is not null ? new UserId(_authUserContext.User.Id) : null;
        if (userId is null)
        {
            return Unauthorized();
        }

        var command = new CreateAutomationCommand(
            userId,
            request.Label,
            request.Description,
            _mapper.Map<CreateAutomationTriggerCommand>(request.Trigger),
            request.Actions.Select(a => _mapper.Map<CreateAutomationActionCommand>(a)).ToList(),
            request.Enabled
        );

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

    [AllowAnonymous]
    [HttpGet("schema", Name = "GetAutomationSchema")]
    [ProducesResponseType<ProvidersSettings>(StatusCodes.Status200OK)]
    public Task<IActionResult> GetAutomationSchema()
    {
        return Task.FromResult<IActionResult>(Ok(_providersSettings));
    }
}
