using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.OpenAi.ValueObjects;

public class OpenAiModelId : ValueObject
{
    public OpenAiModelId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
