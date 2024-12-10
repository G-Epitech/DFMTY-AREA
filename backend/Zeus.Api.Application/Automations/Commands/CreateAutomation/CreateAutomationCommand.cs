using ErrorOr;

using MediatR;

using Zeus.Api.Domain.AutomationAggregate;

namespace Zeus.Api.Application.Automations.Commands.CreateAutomation;

public record CreateAutomationCommand(
    Guid UserId) : IRequest<ErrorOr<Automation>>;
