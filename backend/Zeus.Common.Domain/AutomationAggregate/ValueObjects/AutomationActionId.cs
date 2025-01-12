﻿using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationActionId : ValueObject
{
    public AutomationActionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get;}

    public static AutomationActionId CreateUnique()
    {
        return new AutomationActionId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
