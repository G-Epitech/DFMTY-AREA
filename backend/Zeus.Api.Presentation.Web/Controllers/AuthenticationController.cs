using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Authentication.Commands.Register;
using Zeus.Api.Application.Authentication.Queries.Login;
using Zeus.Api.Presentation.Web.Contracts.Authentication;
using Zeus.Api.Presentation.Web.Controllers.Users;

namespace Zeus.Api.Presentation.Web.Controllers;

[AllowAnonymous]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("register", Name = "Register")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await _sender.Send(command);

        return authResult.Match(
            result => CreatedAtRoute(nameof(UserController.GetAuthUser),
                _mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }

    [HttpPost("login", Name = "Login")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _sender.Send(query);

        return authResult.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }
}
