using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.CreateGithubIntegration;

public record CreateGithubIntegrationCommand(
    string Code,
    string State) : IRequest<ErrorOr<CreateGithubIntegrationCommandResult>>;
