
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Authentication.ValueObjects;

public abstract class Token : ValueObject
{
    public string Value { get; }

    protected Token(string token)
    {
        Value = token;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
