using Mapster;

using Zeus.Api.Integration.Contracts;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Daemon.Infrastructure.Integrations.Api.Mapping;

public class AutomationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AutomationCreatedEventAction, AutomationAction>()
            .Map(x => x.Dependencies, x => x.Dependencies.Select(y => new IntegrationId(y)).ToList());

        config.NewConfig<AutomationCreatedEventTrigger, AutomationTrigger>()
            .Map(x => x.Dependencies, x => x.Dependencies.Select(y => new IntegrationId(y)).ToList());
    }
}
