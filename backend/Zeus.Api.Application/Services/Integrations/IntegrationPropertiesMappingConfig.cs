using Mapster;

using Zeus.Api.Domain.Integrations.Discord;
using Zeus.Api.Domain.Integrations.Notion;
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

        config.NewConfig<NotionBot, IntegrationNotionProperties>()
            .Map(dest => dest.Id, src => src.Owner.Id.Value)
            .Map(dest => dest.AvatarUri, src => src.Owner.AvatarUri)
            .Map(dest => dest.Name, src => src.Owner.Name)
            .Map(dest => dest.Email, src => src.Owner.Email)
            .Map(dest => dest.WorkspaceName, src => src.WorkspaceName);
    }
}
