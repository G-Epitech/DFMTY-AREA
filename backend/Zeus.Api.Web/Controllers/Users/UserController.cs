using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Automations.Query.GetAutomations;
using Zeus.Api.Application.Integrations.Query.GetIntegrations;
using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Application.Users.Query;
using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Web.Contracts.Automations;
using Zeus.Api.Web.Contracts.Common;
using Zeus.Api.Web.Contracts.Integrations;
using Zeus.Api.Web.Contracts.Users;
using Zeus.Api.Web.Mapping;

namespace Zeus.Api.Web.Controllers.Users;

[Route("user", Name = "User")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IAuthUserContext _authUserContext;

    public UserController(ISender sender, IMapper mapper, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _mapper = mapper;
        _authUserContext = authUserContext;
    }

    [HttpGet(Name = "GetAuthUser")]
    [ProducesResponseType<GetUserResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthUser()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var userResult = await _sender.Send(new GetUserQuery(authUser.Id));

        return userResult.Match(
            result => Ok(_mapper.Map<GetUserResponse>(result)),
            Problem);
    }

    [HttpGet("integrations", Name = "GetAuthUserIntegrations")]
    [ProducesResponseType<PageResponse<GetIntegrationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthUserIntegrations([FromQuery] int page = 0, [FromQuery] int size = 10)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var integrationsResult = await _sender.Send(new GetIntegrationsQuery(authUser.Id, page, size));
        if (integrationsResult.IsError)
        {
            return Problem(integrationsResult.Errors);
        }

        var integrationResponses = new List<GetIntegrationResponse>();

        foreach (GetIntegrationQueryResult integrationQueryResult in integrationsResult.Value.Items)
        {
            var propertiesResponse = _mapper.MapIntegrationPropertiesResponse(integrationQueryResult);
            var integrationResponse = new GetIntegrationResponse(
                integrationQueryResult.Id,
                integrationQueryResult.OwnerId,
                integrationQueryResult.Type,
                integrationQueryResult.IsValid,
                propertiesResponse);

            integrationResponses.Add(integrationResponse);
        }

        var httpResponse = new PageResponse<GetIntegrationResponse>(
            integrationsResult.Value.Index,
            integrationsResult.Value.Size,
            integrationsResult.Value.TotalPages,
            integrationsResult.Value.TotalItems,
            integrationResponses.ToArray());

        return Ok(httpResponse);
    }

    [HttpGet("automations", Name = "GetAuthUserAutomations")]
    [ProducesResponseType<PageResponse<GetAutomationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthUserAutomations([FromQuery] int page = 0, [FromQuery] int size = 10)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var automationsResult = await _sender.Send(new GetAutomationsQuery(authUser.Id, page, size));
        if (automationsResult.IsError)
        {
            return Problem(automationsResult.Errors);
        }

        var automationsResponses = new List<GetAutomationResponse>();

        foreach (Automation automation in automationsResult.Value.Items)
        {
            var automationResponse = _mapper.Map<GetAutomationResponse>(automation);

            automationsResponses.Add(automationResponse);
        }

        var httpResponse = new PageResponse<GetAutomationResponse>(
            automationsResult.Value.Index,
            automationsResult.Value.Size,
            automationsResult.Value.TotalPages,
            automationsResult.Value.TotalItems,
            automationsResponses.ToArray());

        return Ok(httpResponse);
    }
}
