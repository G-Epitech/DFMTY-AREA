using Mapster;

using Zeus.Api.Application.Integrations.Query.GetIntegration.Results;
using Zeus.Api.Domain.Integrations.Discord;

namespace Zeus.Api.Application.Integrations.Query.GetIntegration;

public class GetIntegrationQueryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DiscordUser, GetDiscordIntegrationPropertiesQueryResult>()
            .Map(dest => dest.Id, src => src.Id.ValueString)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Flags, src => src.Flags)
            .Map(dest => dest.DisplayName, src => src.DisplayName)
            .Map(dest => dest.AvatarUri, src => src.AvatarUri);
    }
}
