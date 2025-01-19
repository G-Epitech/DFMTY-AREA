using Zeus.Common.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.Presentation.Web.Contracts.Automations;

public record CreateAutomationRequest(
    string Label,
    string Description,
    string Color,
    string Icon,
    CreateAutomationTriggerRequest Trigger,
    CreateAutomationActionRequest[] Actions,
    bool Enabled = true
);

public record CreateAutomationTriggerRequest(
    string Identifier,
    CreateAutomationTriggerParameterRequest[] Parameters,
    Guid[] Dependencies
);

public record CreateAutomationTriggerParameterRequest(
    string Identifier,
    string Value
);

public record CreateAutomationActionRequest(
    string Identifier,
    CreateAutomationActionParameterRequest[] Parameters,
    Guid[] Dependencies
);

public record CreateAutomationActionParameterRequest(
    string Identifier,
    string Value,
    AutomationActionParameterType Type
);
