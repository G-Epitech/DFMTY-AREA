using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Query.GetDiscordGuilds;

public record GetDiscordGuildsQuery(
    Guid UserId,
    Guid IntegrationId) : IRequest<ErrorOr<List<GetDiscordGuildQueryResult>>>;
