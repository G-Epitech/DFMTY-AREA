using ErrorOr;

using MediatR;

using Zeus.Common.Domain.AutomationAggregate;

namespace Zeus.Api.Application.Automations.Commands.CreateAutomation;

public record CreateAutomationCommand(
    Guid UserId) : IRequest<ErrorOr<Automation>>;
