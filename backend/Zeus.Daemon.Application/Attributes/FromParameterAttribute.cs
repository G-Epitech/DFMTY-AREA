namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromParameterAttribute : Attribute
{
    public string? Identifier { get; }

    public FromParameterAttribute(string? identifier = null)
    {
        Identifier = identifier;
    }
}
