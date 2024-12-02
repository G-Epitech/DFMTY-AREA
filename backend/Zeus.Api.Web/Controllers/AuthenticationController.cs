using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Authentication.Commands.Register;
using Zeus.Api.Application.Authentication.Queries.Login;
using Zeus.Api.Web.Contracts.Authentication;

using RegisterRequest = Zeus.Api.Web.Contracts.Authentication.RegisterRequest;

namespace Zeus.Api.Web.Controllers;

[Controller]
[AllowAnonymous]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register", Name = "Register")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _mediator.Send(command);

        return authResult.Match(
            result =>
            {
                var locationUrl = Url.Action("", new { id = result.UserId.Value });

                return Created(locationUrl + result.UserId.Value, _mapper.Map<AuthenticationResponse>(result));
            },
            Problem);
    }

    [HttpPost("login", Name = "Login")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(command);

        return authResult.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }
}
