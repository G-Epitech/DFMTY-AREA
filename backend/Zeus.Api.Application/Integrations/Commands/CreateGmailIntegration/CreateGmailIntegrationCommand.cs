using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.CreateGmailIntegration;

public record CreateGmailIntegrationCommand(
    string Code,
    string State) : IRequest<ErrorOr<CreateGmailIntegrationCommandResult>>;
