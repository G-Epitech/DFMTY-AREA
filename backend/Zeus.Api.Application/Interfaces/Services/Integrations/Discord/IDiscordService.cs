using ErrorOr;

using Zeus.Api.Domain.Integrations.Discord.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Services.Integrations.Discord;

public interface IDiscordService
{
    public Task<ErrorOr<DiscordUserTokens>> GetTokensFromOauth2Async(string code);
}
