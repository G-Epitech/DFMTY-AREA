using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspaceDatabases;
using Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspacePages;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.Notion;

[Route("integrations/{integrationId}/notion")]
public class NotionPropsController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public NotionPropsController(ISender sender, IMapper mapper, IAuthUserContext authUserContext)
    {
        _sender = sender;
        _mapper = mapper;
        _authUserContext = authUserContext;
    }

    [HttpGet("databases", Name = "GetNotionWorkspaceDatabases")]
    [ProducesResponseType<List<GetNotionWorkspaceDatabaseResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotionWorkspaceDatabases(Guid integrationId)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var userGuildsResult = await _sender.Send(
            new GetNotionWorkspaceDatabasesQuery(authUser.Id, integrationId));

        return userGuildsResult.Match(
            result => Ok(result.Select(database =>
                _mapper.Map<GetNotionWorkspaceDatabaseResponse>(database))),
            Problem);
    }

    [HttpGet("pages", Name = "GetNotionWorkspacePages")]
    [ProducesResponseType<List<GetNotionWorkspacePageResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotionWorkspacePages(Guid integrationId)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var userGuildsResult = await _sender.Send(
            new GetNotionWorkspacePagesQuery(authUser.Id, integrationId));

        return userGuildsResult.Match(
            result => Ok(result.Select(page =>
                _mapper.Map<GetNotionWorkspacePageResponse>(page))),
            Problem);
    }
}
