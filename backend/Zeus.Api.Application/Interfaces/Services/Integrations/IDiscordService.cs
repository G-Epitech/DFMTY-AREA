using ErrorOr;

using Zeus.Api.Domain.Integrations.Discord;
using Zeus.Api.Domain.Integrations.Discord.ValueObjects;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IDiscordService
{
    /// <summary>
    /// Get new tokens from Oauth2 code for a discord user
    /// </summary>
    /// <param name="code">The code generated by the discord Oauth2 page</param>
    /// <returns>Tokens of the discord user</returns>
    public Task<ErrorOr<DiscordUserTokens>> GetTokensFromOauth2Async(string code);

    /// <summary>
    /// Get a discord user (/users/@me)
    /// </summary>
    /// <param name="accessToken">The access token of the user (build from Oauth2)</param>
    /// <returns>The discord user</returns>
    public Task<ErrorOr<DiscordUser>> GetUserAsync(AccessToken accessToken);

    /// <summary>
    /// Get discord guilds of a user (/users/@me/guilds)
    /// </summary>
    /// <param name="accessToken">The access token of the user (build from Oauth2)</param>
    /// <returns>Discord user guilds</returns>
    public Task<ErrorOr<List<DiscordGuild>>> GetUserGuildsAsync(AccessToken accessToken);

    /// <summary>
    /// Get discord guilds of a bot (/users/@me/guilds)
    /// </summary>
    /// <param name="botToken">The triggo bot token</param>
    /// <returns>Discord bot guilds</returns>
    public Task<ErrorOr<List<DiscordGuild>>> GetBotGuildsAsync(string botToken);

    /// <summary>
    /// Get discord channels of a guild (/guilds/{guild.id}/channels)
    /// </summary>
    /// <param name="guildId">The discord guild id</param>
    /// <param name="botToken">The triggo bot token</param>
    /// <returns>Discord guild channels</returns>
    public Task<ErrorOr<List<DiscordChannel>>> GetGuildChannelsAsync(DiscordGuildId guildId, string botToken);
}