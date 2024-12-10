using Zeus.Daemon.Domain.Automation.AutomationAggregate.Enums;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;

public struct DynamicParameter
{
    public DynamicParameterType Type { get; set; }
    public string Value { get; set; }
    public string Identifier { get; set; }
}
