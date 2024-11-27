using Zeus.Api.Domain.Common.Models;

namespace Zeus.Api.Domain.Common.ValueObjects.Authentication;

public class Token : ValueObject
{
    public string Value { get; }
    
    public Token(string token)
    {
        Value = token;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
