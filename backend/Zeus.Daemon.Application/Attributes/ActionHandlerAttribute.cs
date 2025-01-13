namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ActionHandlerAttribute : Attribute
{
    public ActionHandlerAttribute(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }
}
