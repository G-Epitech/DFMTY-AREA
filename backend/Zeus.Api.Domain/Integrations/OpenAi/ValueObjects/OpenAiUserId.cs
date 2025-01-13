using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.OpenAi.ValueObjects;

public class OpenAiUserId : ValueObject
{
    public string Value { get; }
    
    public OpenAiUserId(string value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
