namespace Zeus.Common.Domain.ProvidersSettings;

public enum IntegrationRequirements
{
    Single = 0,
    OneOrMore = 1,
    Multiple = 2
}

public class IntegrationRuleSchema
{
    public IntegrationRuleSchema(IntegrationRequirements require, bool optional)
    {
        Require = require;
        Optional = optional;
    }

    public IntegrationRequirements Require { get; }
    public bool Optional { get; }
}
