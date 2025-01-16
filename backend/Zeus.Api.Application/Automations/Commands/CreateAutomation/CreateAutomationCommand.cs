using ErrorOr;

using MediatR;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Automations.Commands.CreateAutomation;

public record CreateAutomationCommand(
    UserId OwnerId,
    string Label,
    string Description,
    CreateAutomationTriggerCommand Trigger,
    List<CreateAutomationActionCommand> Actions,
    bool Enabled = true
) : IRequest<ErrorOr<Automation>>;

public record CreateAutomationTriggerCommand(
    string Identifier,
    List<CreateAutomationTriggerParameterCommand> Parameters,
    List<Guid> Dependencies
);

public record CreateAutomationTriggerParameterCommand(
    string Identifier,
    string Value
);

public record CreateAutomationActionCommand(
    string Identifier,
    List<CreateAutomationActionParameterRequest> Parameters,
    List<Guid> Dependencies
);

public record CreateAutomationActionParameterRequest(
    string Identifier,
    string Value,
    AutomationActionParameterType Type
);
