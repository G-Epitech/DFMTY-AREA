using Zeus.Common.Domain.Integrations.Common.Enums;

namespace Zeus.Common.Domain.ProvidersSettings;

public interface IHasDependenciesSchema
{
    public Dictionary<IntegrationType, DependencyRuleSchema> Dependencies { get; }
}
