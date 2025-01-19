using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.GenerateGithubOauth2Uri;

public record GenerateGithubOauth2UriCommand(
    Guid UserId) : IRequest<ErrorOr<GenerateGithubOauth2UriCommandResult>>;
