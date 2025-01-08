namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromParametersAttribute : Attribute
{
    public string? Identifier { get; }

    public FromParametersAttribute(string? identifier = null)
    {
        Identifier = identifier;
    }
}
