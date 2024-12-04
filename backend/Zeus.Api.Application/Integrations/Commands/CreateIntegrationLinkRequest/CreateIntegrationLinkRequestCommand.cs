using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;

public record CreateIntegrationLinkRequestCommand(
    Guid UserId,
    CreateIntegrationLinkRequestCommandType Type) : IRequest<ErrorOr<CreateIntegrationLinkRequestCommandResult>>;
