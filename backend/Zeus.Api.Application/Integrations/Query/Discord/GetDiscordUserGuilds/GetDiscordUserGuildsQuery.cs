using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Query.Discord.GetDiscordUserGuilds;

public record GetDiscordUserGuildsQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<List<GetDiscordUserGuildQueryResult>>>;
