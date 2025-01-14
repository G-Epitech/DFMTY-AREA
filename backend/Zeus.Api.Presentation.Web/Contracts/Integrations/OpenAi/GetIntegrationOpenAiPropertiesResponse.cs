namespace Zeus.Api.Presentation.Web.Contracts.Integrations.OpenAi;

public record GetIntegrationOpenAiPropertiesResponse(
    string OwnerId,
    string OwnerName,
    string OwnerEmail) : GetIntegrationPropertiesResponse;
