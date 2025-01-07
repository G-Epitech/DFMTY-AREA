namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TriggerIdentifierAttribute : Attribute
{
    public string Identifier { get; }

    public TriggerIdentifierAttribute(string identifier)
    {
        Identifier = identifier;
    }
}

