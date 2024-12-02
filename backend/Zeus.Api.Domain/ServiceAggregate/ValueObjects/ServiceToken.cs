using Zeus.Api.Domain.ServiceAggregate.Enums;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.ServiceAggregate.ValueObjects;

public sealed class ServiceToken : ValueObject
{
    public string Value { get; }

    public string Type { get; }

    public ServiceTokenUsage Usage { get; }

    public ServiceToken(string value, string type, ServiceTokenUsage usage)
    {
        Value = value;
        Type = type;
        Usage = usage;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Type;
        yield return Usage;
    }
}
