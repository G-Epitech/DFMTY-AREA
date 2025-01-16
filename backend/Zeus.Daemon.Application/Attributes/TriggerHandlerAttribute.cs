using JetBrains.Annotations;

namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]

public class TriggerHandlerAttribute : Attribute
{
    public TriggerHandlerAttribute(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }
}

