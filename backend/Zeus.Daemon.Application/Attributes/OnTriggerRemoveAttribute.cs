using JetBrains.Annotations;

namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
[MeansImplicitUse(ImplicitUseKindFlags.Access)]
public class OnTriggerRemoveAttribute : Attribute;

