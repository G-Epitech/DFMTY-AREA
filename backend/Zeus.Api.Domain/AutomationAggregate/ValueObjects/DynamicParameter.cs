using Zeus.Api.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.Domain.AutomationAggregate.ValueObjects;

public struct DynamicParameter
{
    public DynamicParameterType Type { get; set; }
    public string Value { get; set; }
    public string Identifier { get; set; }
}
