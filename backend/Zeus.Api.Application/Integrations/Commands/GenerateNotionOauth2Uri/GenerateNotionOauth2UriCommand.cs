using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.GenerateNotionOauth2Uri;

public record GenerateNotionOauth2UriCommand(
    Guid UserId) : IRequest<ErrorOr<GenerateNotionOauth2UriCommandResult>>;
