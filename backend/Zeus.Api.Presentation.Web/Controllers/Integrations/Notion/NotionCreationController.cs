using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.GenerateNotionOauth2Uri;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.Notion;

[Route("integrations/notion")]
public class NotionCreationController : ApiController
{
    private readonly ISender _sender;
    private readonly IAuthUserContext _authUserContext;

    public NotionCreationController(ISender sender, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _authUserContext = authUserContext;
    }

    [HttpPost("uri", Name = "GenerateNotionUri")]
    [ProducesResponseType<GenerateNotionUriResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateNotionUri()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var generateUriResult = await _sender.Send(new GenerateNotionOauth2UriCommand(
            authUser.Id));

        return generateUriResult.Match(
            result => Ok(new GenerateNotionUriResponse(result.Uri.ToString())),
            Problem);
    }
}
