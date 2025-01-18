using Mapster;

using Zeus.Api.Presentation.gRPC.Contracts;

using IntegrationType = Zeus.Common.Domain.Integrations.Common.Enums.IntegrationType;

namespace Zeus.Api.Presentation.gRPC.Mappings;

using Integration = Common.Domain.Integrations.IntegrationAggregate.Integration;
using IntegrationType = IntegrationType;

public class IntegrationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Integration, Contracts.Integration>()
            .MapWith(i => new Contracts.Integration
            {
                Id = i.Id.Value.ToString(),
                OwnerId = i.OwnerId.Value.ToString(),
                ClientId = i.ClientId,
                Type = MapIntegrationType(i.Type),
                Tokens = { Enumerable.Select(i.Tokens, t => new IntegrationToken { Value = t.Value, Type = t.Type, Usage = MapTokenUsage(t.Usage) }).ToList() },
                CreatedAt = new DateTimeOffset(i.CreatedAt.ToUniversalTime()).ToUnixTimeSeconds(),
                UpdatedAt = new DateTimeOffset(i.UpdatedAt.ToUniversalTime()).ToUnixTimeSeconds()
            });
    }

    private static IntegrationTokenUsage MapTokenUsage(Common.Domain.Integrations.IntegrationAggregate.Enums.IntegrationTokenUsage usage)
    {
        return usage switch
        {
            Common.Domain.Integrations.IntegrationAggregate.Enums.IntegrationTokenUsage.Access => IntegrationTokenUsage.Access,
            Common.Domain.Integrations.IntegrationAggregate.Enums.IntegrationTokenUsage.Refresh => IntegrationTokenUsage.Refresh,
            _ => throw new ArgumentOutOfRangeException(nameof(usage), usage, null)
        };
    }

    private static Contracts.IntegrationType MapIntegrationType(IntegrationType type)
    {
        return type switch
        {
            IntegrationType.Discord => Contracts.IntegrationType.Discord,
            IntegrationType.Gmail => Contracts.IntegrationType.Gmail,
            IntegrationType.Notion => Contracts.IntegrationType.Notion,
            IntegrationType.OpenAi => Contracts.IntegrationType.OpenAi,
            IntegrationType.LeagueOfLegends => Contracts.IntegrationType.LeagueOfLegends,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
