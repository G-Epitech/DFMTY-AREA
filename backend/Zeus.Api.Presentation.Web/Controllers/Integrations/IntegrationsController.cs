using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Query.Integrations.GetIntegration;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations;
using Zeus.Api.Presentation.Web.Mapping;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations;

[Route("integrations")]
public class IntegrationsController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

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
