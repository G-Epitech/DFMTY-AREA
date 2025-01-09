namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TriggerHandlerAttribute : Attribute
{
    public string Identifier { get; }

    public TriggerHandlerAttribute(string identifier)
    {
        Identifier = identifier;
    }
}

