﻿using System.Diagnostics.CodeAnalysis;

namespace Zeus.Common.Domain.ProvidersSettings;

public enum DependencyRequirements
{
    Single = 0,
    OneOrMore = 1,
    Multiple = 2
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class DependencyRuleSchema
{
    public DependencyRuleSchema(DependencyRequirements require, bool optional)
    {
        Require = require;
        Optional = optional;
    }

    public DependencyRequirements Require { get; }
    public bool Optional { get; }
}
