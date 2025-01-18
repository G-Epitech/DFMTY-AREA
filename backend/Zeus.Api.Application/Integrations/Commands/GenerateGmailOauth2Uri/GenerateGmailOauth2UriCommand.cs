using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.GenerateGmailOauth2Uri;

public record GenerateGmailOauth2UriCommand(
    Guid UserId) : IRequest<ErrorOr<GenerateGmailOauth2UriCommandResult>>;
