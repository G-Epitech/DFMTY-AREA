using MapsterMapper;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Domain.Integrations.Properties;
using Zeus.Api.Presentation.Web.Contracts.Integrations;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

namespace Zeus.Api.Presentation.Web.Mapping;

public static class IntegrationPropertiesMappingExtention
{
    public static GetIntegrationPropertiesResponse MapIntegrationPropertiesResponse(this IMapper mapper,
        GetIntegrationWithPropertiesQueryResult integrationWithPropertiesQueryResult)
    {
        return integrationWithPropertiesQueryResult.Properties switch
        {
            IntegrationDiscordProperties discord =>
                mapper.Map<GetIntegrationDiscordPropertiesResponse>(discord),
            IntegrationNotionProperties notion =>
                mapper.Map<GetIntegrationNotionPropertiesResponse>(notion),
            _ => throw new ArgumentOutOfRangeException(nameof(integrationWithPropertiesQueryResult.Properties))
        };
    }
}
