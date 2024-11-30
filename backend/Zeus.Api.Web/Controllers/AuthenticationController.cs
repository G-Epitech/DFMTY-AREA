using ErrorOr;

using MapsterMapper;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Api.Application.Authentication.Commands.Register;
using Zeus.Api.Web.Contracts.Authentication;

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
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }
}
