using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Notion.ValueObjects;

public class NotionDatabaseId : ValueObject
{
    public string Value { get; }
    
    public NotionDatabaseId(string value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
