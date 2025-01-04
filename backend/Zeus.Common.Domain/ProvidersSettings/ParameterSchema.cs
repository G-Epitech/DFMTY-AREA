namespace Zeus.Common.Domain.ProvidersSettings;

public class ParameterSchema
{
    public string Name { get; }
    public string Description { get; }
    public VarType Type { get; }

    public ParameterSchema(string name, string description, VarType type)
    {
        Name = name;
        Description = description;
        Type = type;
    }
}
