using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.OpenAi.ValueObjects;

public class OpenAiModelId : ValueObject
{
    public string Value { get; }
    
    public OpenAiModelId(string value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
