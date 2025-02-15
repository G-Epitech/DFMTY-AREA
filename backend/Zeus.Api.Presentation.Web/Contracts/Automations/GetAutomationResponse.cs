namespace Zeus.Api.Presentation.Web.Contracts.Automations;

public record GetAutomationResponse(
    Guid Id,
    string Label,
    string Description,
    string Color,
    string Icon,
    Guid OwnerId,
    GetAutomationTriggerResponse Trigger,
    GetAutomationActionResponse[] Actions,
    bool Enabled,
    DateTime UpdatedAt,
    DateTime CreatedAt);

public record GetAutomationTriggerResponse(
    string Identifier,
    GetAutomationTriggerParameterResponse[] Parameters,
    Guid[] Dependencies);

public record GetAutomationTriggerParameterResponse(
    string Identifier,
    string Value);

public record GetAutomationActionResponse(
    string Identifier,
    GetAutomationActionParameterResponse[] Parameters,
    Guid[] Dependencies);

public record GetAutomationActionParameterResponse(
    GetAutomationActionParameterTypeResponse Type,
    string Identifier,
    string Value);

public enum GetAutomationActionParameterTypeResponse
{
    Var,
    Raw
}
