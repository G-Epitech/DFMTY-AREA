using Mapster;

using Zeus.Api.Domain.Integrations.Discord;
using Zeus.Api.Domain.Integrations.Properties;

namespace Zeus.Api.Application.Services.Integrations;

public class IntegrationPropertiesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DiscordUser, IntegrationDiscordProperties>()
            .Map(dest => dest.Id, src => src.Id.ValueString)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Flags, src => src.Flags)
            .Map(dest => dest.DisplayName, src => src.DisplayName)
            .Map(dest => dest.AvatarUri, src => src.AvatarUri);
    }
}
