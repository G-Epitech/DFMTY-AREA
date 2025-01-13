using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.OAuth2.Google.ValueObjects;

public class GoogleUserId : ValueObject
{
    public GoogleUserId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
