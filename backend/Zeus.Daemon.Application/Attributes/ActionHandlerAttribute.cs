namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ActionHandlerAttribute : Attribute
{
    public string Identifier { get; }

    public ActionHandlerAttribute(string identifier)
    {
        Identifier = identifier;
    }
}
