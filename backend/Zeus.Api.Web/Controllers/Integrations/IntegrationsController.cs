using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Query.GetIntegration;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Web.Contracts.Integrations;
using Zeus.Api.Web.Mapping;

namespace Zeus.Api.Web.Controllers.Integrations;

[Route("integrations")]
public class IntegrationsController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IAuthUserContext _authUserContext;

    public IntegrationsController(ISender sender, IAuthUserContext authUserContext, IMapper mapper)
    {
        _sender = sender;
        _authUserContext = authUserContext;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}", Name = "GetIntegrationById")]
    [ProducesResponseType<GetIntegrationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIntegrationById(Guid id)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var getIntegrationResult = await _sender.Send(new GetIntegrationQuery(authUser.Id, id));
        if (getIntegrationResult.IsError)
        {
            return Problem(getIntegrationResult.Errors);
        }

        var propertiesResponse = _mapper.MapIntegrationPropertiesResponse(getIntegrationResult.Value);

        var httpResponse = new GetIntegrationResponse(
            getIntegrationResult.Value.Id,
            getIntegrationResult.Value.OwnerId,
            getIntegrationResult.Value.Type,
            getIntegrationResult.Value.IsValid,
            propertiesResponse);

        return Ok(httpResponse);
    }
}
