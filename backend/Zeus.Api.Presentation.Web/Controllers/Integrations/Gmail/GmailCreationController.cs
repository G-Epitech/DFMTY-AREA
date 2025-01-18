using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Commands.GenerateGmailOauth2Uri;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Gmail;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.Gmail;

[Route("integrations/gmail")]
public class GmailCreationController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly ISender _sender;

    public GmailCreationController(ISender sender, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _authUserContext = authUserContext;
    }

    [HttpPost("uri", Name = "GenerateGmailUri")]
    [ProducesResponseType<GenerateGmailUriResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateGmailUri()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var generateUriResult = await _sender.Send(new GenerateGmailOauth2UriCommand(
            authUser.Id));

        return generateUriResult.Match(
            result => Ok(new GenerateGmailUriResponse(result.Uri.ToString())),
            Problem);
    }

    [AllowAnonymous]
    [HttpPost(Name = "CreateGmailIntegration")]
    public IActionResult CreateGmailIntegration(CreateGmailIntegrationRequest request)
    {
        Console.WriteLine($"Code = {request.Code}, State = {request.State}");
        return Created();
    }
}
