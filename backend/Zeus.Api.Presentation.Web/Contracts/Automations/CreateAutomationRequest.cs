using Zeus.Common.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.Presentation.Web.Contracts.Automations;

public record CreateAutomationRequest(
    string Label,
    string Description,
    CreateAutomationTriggerRequest Trigger,
    CreateAutomationActionRequest[] Actions,
    bool Enabled = true
);

public record CreateAutomationTriggerRequest(
    string Identifier,
    CreateAutomationTriggerParameterRequest[] Parameters,
    Guid[] Providers
);

public record CreateAutomationTriggerParameterRequest(
    string Identifier,
    string Value
);

public record CreateAutomationActionRequest(
    string Identifier,
    CreateAutomationActionParameterRequest[] Parameters,
    Guid[] Providers
);

public record CreateAutomationActionParameterRequest(
    string Identifier,
    string Value,
    AutomationActionParameterType Type
);
