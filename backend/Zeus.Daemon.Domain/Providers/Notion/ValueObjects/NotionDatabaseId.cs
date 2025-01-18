using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

public class NotionDatabaseId : ValueObject
{
    public NotionDatabaseId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
