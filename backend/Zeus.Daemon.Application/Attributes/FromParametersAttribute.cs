namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromParametersAttribute : Attribute
{
    public FromParametersAttribute(string? identifier = null)
    {
        Identifier = identifier;
    }

    public string? Identifier { get; }
}
