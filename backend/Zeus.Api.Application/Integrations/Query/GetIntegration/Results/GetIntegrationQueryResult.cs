namespace Zeus.Api.Application.Integrations.Query.GetIntegration.Results;

public abstract record GetIntegrationPropertiesQueryResult();

public record GetIntegrationQueryResult(
    Guid Id,
    Guid OwnerId,
    string Type,
    bool IsValid,
    GetIntegrationPropertiesQueryResult Properties);
