namespace Zeus.Api.Web.Contracts.Integrations;

public abstract record GetIntegrationPropertiesResponse();

public record GetIntegrationResponse(
    Guid Id,
    Guid OwnerId,
    string Type,
    bool IsValid,
    object Properties);
