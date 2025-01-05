namespace Zeus.Api.Presentation.Web.Contracts.Automations;

public record GetAutomationResponse(
    Guid Id,
    string Label,
    string Description,
    Guid OwnerId,
    GetAutomationTriggerResponse Trigger,
    GetAutomationActionResponse[] Actions,
    bool Enabled,
    DateTime UpdatedAt);

public record GetAutomationTriggerResponse(
    string Identifier,
    GetAutomationTriggerParameterResponse[] Parameters,
    string[] Providers);

public record GetAutomationTriggerParameterResponse(
    string Identifier,
    string Value);

public record GetAutomationActionResponse(
    string Identifier,
    GetAutomationActionParameterResponse[] Parameters,
    string[] Providers);

public record GetAutomationActionParameterResponse(
    GetAutomationActionParameterTypeResponse Type,
    string Identifier,
    string Value);

public enum GetAutomationActionParameterTypeResponse
{
    Var,
    Row
}
