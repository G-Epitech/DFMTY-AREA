namespace Zeus.Api.Domain.Integrations.Properties;

public record IntegrationOpenAiProperties(
    string OwnerId,
    string OwnerName,
    string OwnerEmail) : IntegrationProperties;
