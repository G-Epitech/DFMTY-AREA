using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.OAuth2.Google.ValueObjects;

public class GoogleUserId : ValueObject
{
    public string Value { get; }
    
    public GoogleUserId(string value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
