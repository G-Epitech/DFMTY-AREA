using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class IntegrationToken : ValueObject
{
    public IntegrationToken(string value, string type, IntegrationTokenUsage usage)
    {
        Value = value;
        Type = type;
        Usage = usage;
    }

#pragma warning disable CS8618
    private IntegrationToken()
    {
    }
#pragma warning restore CS8618
    public string Value { get; }

    public string Type { get; }

    public IntegrationTokenUsage Usage { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Type;
        yield return Usage;
    }
}
