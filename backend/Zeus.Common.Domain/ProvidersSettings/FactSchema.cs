namespace Zeus.Common.Domain.ProvidersSettings;

public class FactSchema
{
    public string Name { get; }
    public string Description { get; }
    public VarType Type { get; }

    public FactSchema(string name, string description, VarType type)
    {
        Name = name;
        Description = description;
        Type = type;
    }
}
