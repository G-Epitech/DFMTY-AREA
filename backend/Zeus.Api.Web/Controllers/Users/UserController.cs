using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Users.Query;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Web.Contracts.Users;

namespace Zeus.Api.Web.Controllers.Users;

[Route("user", Name = "User")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IAuthUserContext _authUserContext;

    public UserController(ISender sender, IMapper mapper, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _mapper = mapper;
        _authUserContext = authUserContext;
    }

    [HttpGet(Name = "GetAuthUser")]
    [ProducesResponseType<GetUserResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthUser()
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var userResult = await _sender.Send(new GetUserQuery(authUser.Id));

        return userResult.Match(
            result => Ok(_mapper.Map<GetUserResponse>(result)),
            Problem);
    }
}
