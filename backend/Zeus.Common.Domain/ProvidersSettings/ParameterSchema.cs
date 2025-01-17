using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Common.Enums;

namespace Zeus.Common.Domain.ProvidersSettings;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ParameterSchema
{
    public ParameterSchema(string name, string description, VariableType type)
    {
        Name = name;
        Description = description;
        Type = type;
    }

    public string Name { get; }
    public string Description { get; }
    public VariableType Type { get; }

    public bool IsValidValue(string value)
    {
        return Type switch
        {
            VariableType.String => !string.IsNullOrWhiteSpace(value),
            VariableType.Integer => int.TryParse(value, out _),
            VariableType.Boolean => bool.TryParse(value, out _),
            VariableType.Float => float.TryParse(value, out _),
            VariableType.Datetime => DateTime.TryParse(value, out _),
            VariableType.Object => true,
            _ => false
        };
    }

    public bool IsValidRef(FactSchema refSchema)
    {
        return refSchema.Type == Type;
    }
}
