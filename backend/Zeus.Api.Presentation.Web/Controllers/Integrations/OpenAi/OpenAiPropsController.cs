using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Query.OpenAi.GetOpenAiModels;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.OpenAi;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.OpenAi;

[Route("integrations/{integrationId}/openai")]
public class OpenAiPropsController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public OpenAiPropsController(ISender sender, IMapper mapper, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _mapper = mapper;
        _authUserContext = authUserContext;
    }

    [HttpGet("models", Name = "GetOpenAiModels")]
    [ProducesResponseType<List<GetOpenAiModelResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOpenAiModels(Guid integrationId)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var modelsResult = await _sender.Send(
            new GetOpenAiModelsQuery(authUser.Id, integrationId));

        return modelsResult.Match(
            result => Ok(result.Select(model =>
                new GetOpenAiModelResponse(model.Id))),
            Problem);
    }
}
