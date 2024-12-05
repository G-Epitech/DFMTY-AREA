using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.CreateDiscordIntegration;

public record CreateDiscordIntegrationCommand(
    string Code,
    string State) : IRequest<ErrorOr<CreateDiscordIntegrationCommandResult>>;
