using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Query.GetIntegrations;
using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Application.Users.Query;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Web.Contracts.Common;
using Zeus.Api.Web.Contracts.Integrations;
using Zeus.Api.Web.Contracts.Users;
using Zeus.Api.Web.Mapping;

namespace Zeus.Api.Web.Controllers.Users;

[Route("users", Name = "Users")]
public class UsersController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IAuthUserContext _authUserContext;

    public UsersController(ISender sender, IMapper mapper, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _mapper = mapper;
        _authUserContext = authUserContext;
    }

    [HttpGet("{userId:guid}", Name = "GetUser")]
    [ProducesResponseType<GetUserResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var authUser = _authUserContext.User;
        if (authUser is null || authUser.Id != userId)
        {
            return Unauthorized();
        }

        var userResult = await _sender.Send(new GetUserQuery(userId));

        return userResult.Match(
            result => Ok(_mapper.Map<GetUserResponse>(result)),
            Problem);
    }

    [HttpGet("{userId:guid}/integrations", Name = "GetUserIntegrations")]
    [ProducesResponseType<PageResponse<GetIntegrationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthUserIntegrations(Guid userId, [FromQuery] int page = 0,
        [FromQuery] int size = 10)
    {
        var authUser = _authUserContext.User;
        if (authUser is null || authUser.Id != userId)
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
}
