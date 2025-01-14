using Zeus.Common.Domain.Common.Enums;

namespace Zeus.Common.Domain.ProvidersSettings;

public class ParameterSchema
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
}
