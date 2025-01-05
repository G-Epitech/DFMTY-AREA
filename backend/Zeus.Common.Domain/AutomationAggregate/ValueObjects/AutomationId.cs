﻿using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]

public sealed class AutomationId : ValueObject
{
    public Guid Value { get; }

    public AutomationId(Guid value)
    {
        Value = value;
    }

    public static AutomationId CreateUnique()
    {
        return new AutomationId(Guid.NewGuid());
    }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private AutomationId()
    {
    }
#pragma warning restore CS8618
}