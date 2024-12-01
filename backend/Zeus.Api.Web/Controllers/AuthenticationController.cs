using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Zeus.Api.Application.Authentication.Commands.Register;
using Zeus.Api.Application.Authentication.Queries.Login;
using Zeus.Api.Web.Contracts.Authentication;

using RegisterRequest = Zeus.Api.Web.Contracts.Authentication.RegisterRequest;

namespace Zeus.Api.Web.Controllers;

[Controller]
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

    [HttpPost("register")]
    public Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = _mediator.Send(command);

        return authResult.Match(
            result => Created("/", _mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }

    [HttpPost("login")]
    public Task<IActionResult> Login(LoginRequest request)
    {
        var command = _mapper.Map<LoginQuery>(request);
        var authResult = _mediator.Send(command);
        
        return authResult.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }
}
