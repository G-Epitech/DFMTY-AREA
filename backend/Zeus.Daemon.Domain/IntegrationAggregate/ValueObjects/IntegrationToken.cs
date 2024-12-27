using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.IntegrationAggregate.Enums;

namespace Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;

public sealed class IntegrationToken : ValueObject
{
    public string Value { get; }

    public string Type { get; }

    public IntegrationTokenUsage Usage { get; }

    public IntegrationToken(string value, string type, IntegrationTokenUsage usage)
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

#pragma warning disable CS8618
    private IntegrationToken()
    {
    }
#pragma warning restore CS8618
}
