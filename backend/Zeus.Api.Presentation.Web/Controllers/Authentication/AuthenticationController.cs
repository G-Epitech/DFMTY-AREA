using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Authentication.Commands.PasswordRegister;
using Zeus.Api.Application.Authentication.Queries.PasswordLogin;
using Zeus.Api.Application.Authentication.Queries.RefreshTokens;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Authentication;
using Zeus.Api.Presentation.Web.Controllers.Users;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Presentation.Web.Controllers.Authentication;

[AllowAnonymous]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public AuthenticationController(ISender sender, IMapper mapper, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _mapper = mapper;
        _authUserContext = authUserContext;
    }

    [HttpPost("register", Name = "Register")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<PasswordAuthRegisterCommand>(request);
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
        var query = _mapper.Map<PasswordAuthLoginQuery>(request);
        var authResult = await _sender.Send(query);

        return authResult.Match(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }

    [HttpPost("refresh", Name = "Refresh")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh()
    {
        var userId = _authUserContext.User is not null ? new UserId(_authUserContext.User.Id) : null;
        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _sender.Send(new RefreshTokensQuery(userId));

        if (result.IsError)
        {
            return Unauthorized();
        }

        return Ok(_mapper.Map<AuthenticationResponse>(result.Value));
    }
}
