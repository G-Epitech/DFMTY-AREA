using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Query.Discord.GetDiscordGuildChannels;

public record GetDiscordGuildChannelsQuery(
    Guid UserId,
    Guid IntegrationId,
    string GuildId) : IRequest<ErrorOr<List<GetDiscordGuildChannelQueryResult>>>;
