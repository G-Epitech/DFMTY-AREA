using Zeus.Api.Domain.Integrations.OpenAi.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.OpenAi;

public class OpenAiModel : Entity<OpenAiModelId>
{
    public string Object { get; }
    public DateTime CreatedAt { get; }
    public string OwnedBy { get; }

    public OpenAiModel(OpenAiModelId id, string obj, DateTime createdAt, string ownedBy) : base(id)
    {
        Object = obj;
        CreatedAt = createdAt;
        OwnedBy = ownedBy;
    }
}
