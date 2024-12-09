using Zeus.Api.Domain.Integrations.Properties;

namespace Zeus.Api.Application.Integrations.Query.Results;

public record GetIntegrationQueryResult(
    Guid Id,
    Guid OwnerId,
    string Type,
    bool IsValid,
    IntegrationProperties Properties);
