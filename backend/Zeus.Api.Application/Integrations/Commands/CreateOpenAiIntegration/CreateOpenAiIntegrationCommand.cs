using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.CreateOpenAiIntegration;

public record CreateOpenAiIntegrationCommand(
    Guid UserId,
    string ApiToken,
    string AdminApiToken) : IRequest<ErrorOr<CreateOpenAiIntegrationCommandResult>>;
