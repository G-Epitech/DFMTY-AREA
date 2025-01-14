using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Integrations.Query.Discord.GetDiscordGuildChannels;
using Zeus.Api.Application.Integrations.Query.Discord.GetDiscordUserGuilds;
using Zeus.Api.Infrastructure.Authentication.Context;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;

namespace Zeus.Api.Presentation.Web.Controllers.Integrations.Discord;

[Route("integrations/{integrationId}/discord")]
public class DiscordPropsController : ApiController
{
    private readonly IAuthUserContext _authUserContext;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public DiscordPropsController(ISender sender, IAuthUserContext authUserContext, IMapper mapper)
    {
        _sender = sender;
        _authUserContext = authUserContext;
        _mapper = mapper;
    }

    [HttpGet("guilds", Name = "GetDiscordGuilds")]
    [ProducesResponseType<List<GetDiscordGuildResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDiscordGuilds(Guid integrationId)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var userGuildsResult = await _sender.Send(new GetDiscordUserGuildsQuery(authUser.Id, integrationId));

        return userGuildsResult.Match(
            result => Ok(result.Select(guild => _mapper.Map<GetDiscordGuildResponse>(guild))),
            Problem);
    }

    [HttpGet("guilds/{guildId}/channels", Name = "GetDiscordGuildChannels")]
    [ProducesResponseType<List<GetDiscordGuildChannelResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDiscordGuildChannels(string guildId, Guid integrationId)
    {
        var authUser = _authUserContext.User;
        if (authUser is null)
        {
            return Unauthorized();
        }

        var guildChannels = await _sender.Send(new GetDiscordGuildChannelsQuery(authUser.Id, integrationId, guildId));

        return guildChannels.Match(
            result => Ok(result.Select(guild => _mapper.Map<GetDiscordGuildChannelResponse>(guild))),
            Problem);
    }
}
