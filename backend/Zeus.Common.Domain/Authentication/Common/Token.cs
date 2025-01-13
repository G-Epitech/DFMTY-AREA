
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.Authentication.Common;

public abstract class Token : ValueObject
{
    protected Token(string token)
    {
        Value = token;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
