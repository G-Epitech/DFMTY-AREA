using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.GenerateDiscordOauth2Uri;

public record GenerateDiscordOauth2UriCommand(
    Guid UserId) : IRequest<ErrorOr<GenerateDiscordOauth2UriCommandResult>>;
