using Zeus.Api.Domain.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.IntegrationAggregate.ValueObjects;

public sealed class IntegrationToken : ValueObject
{
    public string Value { get; }

    public string Type { get; }

    public ServiceTokenUsage Usage { get; }

    public IntegrationToken(string value, string type, ServiceTokenUsage usage)
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
