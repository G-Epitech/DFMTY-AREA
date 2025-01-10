using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.CreateNotionIntegration;

public record CreateNotionIntegrationCommand(
    string Code,
    string State) : IRequest<ErrorOr<CreateNotionIntegrationCommandResult>>;
