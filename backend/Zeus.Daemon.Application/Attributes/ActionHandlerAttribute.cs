using JetBrains.Annotations;

namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
[MeansImplicitUse(ImplicitUseKindFlags.Access | ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
public class ActionHandlerAttribute : Attribute
{
    public ActionHandlerAttribute(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }
}
