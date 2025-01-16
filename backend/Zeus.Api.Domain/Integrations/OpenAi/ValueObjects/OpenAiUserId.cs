using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.OpenAi.ValueObjects;

public class OpenAiUserId : ValueObject
{
    public OpenAiUserId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
