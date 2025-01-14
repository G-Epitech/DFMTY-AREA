using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate;

public sealed class OpenAiIntegration : Integration
{
    private OpenAiIntegration(
        IntegrationId id,
        UserId ownerId,
        string clientId,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, IntegrationType.OpenAi, ownerId, clientId, updatedAt, createdAt)
    {
    }

#pragma warning disable CS8618
    private OpenAiIntegration()
    {
    }
#pragma warning restore CS8618

    public override bool IsValid
    {
        get
        {
            return
                _tokens.Any(x => x.Usage == IntegrationTokenUsage.Access);
        }
    }

    public static OpenAiIntegration Create(UserId ownerId, string clientId)
    {
        return new OpenAiIntegration(
            IntegrationId.CreateUnique(),
            ownerId,
            clientId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
