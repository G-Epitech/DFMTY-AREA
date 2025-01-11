using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Notion.ValueObjects;

public class NotionUserId : ValueObject
{
    public string Value { get; }

    public NotionUserId(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
