using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Notion.ValueObjects;

public class NotionIntegrationId : ValueObject
{
    public NotionIntegrationId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
