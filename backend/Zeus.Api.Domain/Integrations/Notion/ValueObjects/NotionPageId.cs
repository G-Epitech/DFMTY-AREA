using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Notion.ValueObjects;

public class NotionPageId : ValueObject
{
    public string Value { get; }

    public NotionPageId(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
