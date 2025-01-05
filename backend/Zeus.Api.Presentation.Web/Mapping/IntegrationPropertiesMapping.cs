using MapsterMapper;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Domain.Integrations.Properties;
using Zeus.Api.Presentation.Web.Contracts.Integrations;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;

namespace Zeus.Api.Presentation.Web.Mapping;

public static class IntegrationPropertiesMappingExtention
{
    public static GetIntegrationPropertiesResponse MapIntegrationPropertiesResponse(this IMapper mapper,
        GetIntegrationQueryResult integrationQueryResult)
    {
        return integrationQueryResult.Properties switch
        {
            IntegrationDiscordProperties discord =>
                mapper.Map<GetIntegrationDiscordPropertiesResponse>(discord),
            _ => throw new ArgumentOutOfRangeException(nameof(integrationQueryResult.Properties))
        };
    }
}
