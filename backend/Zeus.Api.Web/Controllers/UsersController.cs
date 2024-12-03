using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Users.Query;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Web.Contracts.Users;

namespace Zeus.Api.Web.Controllers;

[Route("users", Name = "Users")]
public class UsersController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IAuthUserContext _authUserContext;

    public UsersController(ISender sender, IMapper mapper, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _mapper = mapper;
        _authUserContext = authUserContext;
    }

    [HttpGet("{userId:guid}", Name = "GetUser")]
    [ProducesResponseType<GetUserResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var authUser = _authUserContext.User;
        if (authUser is null || authUser.Id != userId)
        {
            return Unauthorized();
        }
        
        var userResult = await _sender.Send(new GetUserQuery(userId));

        return userResult.Match(
            result => Ok(_mapper.Map<GetUserResponse>(result)),
            Problem);
    }
}
