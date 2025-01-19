using MapsterMapper;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Domain.Integrations.Properties;
using Zeus.Api.Presentation.Web.Contracts.Integrations;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Github;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Gmail;
using Zeus.Api.Presentation.Web.Contracts.Integrations.LeagueOfLegends;
using Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;
using Zeus.Api.Presentation.Web.Contracts.Integrations.OpenAi;

namespace Zeus.Api.Presentation.Web.Mapping;

public static class IntegrationPropertiesMappingExtention
{
    public static GetIntegrationPropertiesResponse MapIntegrationPropertiesResponse(this IMapper mapper,
        GetIntegrationWithPropertiesQueryResult result)
    {
        return result.Properties switch
        {
            IntegrationDiscordProperties discord =>
                mapper.Map<GetIntegrationDiscordPropertiesResponse>(discord),
            IntegrationNotionProperties notion =>
                mapper.Map<GetIntegrationNotionPropertiesResponse>(notion),
            IntegrationOpenAiProperties openAi =>
                mapper.Map<GetIntegrationOpenAiPropertiesResponse>(openAi),
            IntegrationLeagueOfLegendsProperties leagueOfLegends =>
                mapper.Map<GetIntegrationLeagueOfLegendsPropertiesResponse>(leagueOfLegends),
            IntegrationGithubProperties github =>
                mapper.Map<GetIntegrationGithubPropertiesResponse>(github),
            IntegrationGmailProperties gmail =>
                mapper.Map<GetIntegrationGmailPropertiesResponse>(gmail),
            _ => throw new ArgumentOutOfRangeException(nameof(result.Properties))
        };
    }
}
