﻿using Mapster;

using Zeus.Api.Presentation.gRPC.Contracts;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

using Automation = Zeus.Common.Domain.AutomationAggregate.Automation;
using AutomationTriggerParameter = Zeus.Common.Domain.AutomationAggregate.ValueObjects.AutomationTriggerParameter;

namespace Zeus.Api.Presentation.gRPC.Mappings;

public class AutomationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Automation, Contracts.Automation>()
            .MapWith(a => new Contracts.Automation
            {
                Id = a.Id.Value.ToString(),
                Label = a.Label,
                Description = a.Description,
                CreatedAt = new DateTimeOffset(a.CreatedAt.ToUniversalTime()).ToUnixTimeSeconds(),
                UpdatedAt = new DateTimeOffset(a.UpdatedAt.ToUniversalTime()).ToUnixTimeSeconds(),
                Enabled = a.Enabled,
                OwnerId = a.OwnerId.Value.ToString(),
                Actions =
                {
                    a.Actions.Select(action => new AutomationAction
                    {
                        Id = action.Id.Value.ToString(),
                        Identifier = action.Identifier,
                        Parameters =
                        {
                            action.Parameters.Select(p =>
                                new AutomationActionParameter { Identifier = p.Identifier, Type = p.Type.ToString(), Value = p.Value })
                        },
                        Dependencies = { action.Dependencies.Select(p => p.Value.ToString()) },
                        Rank = action.Rank,
                    })
                },
                Trigger = new AutomationTrigger()
                {
                    Id = a.Trigger.Id.Value.ToString(),
                    Identifier = a.Trigger.Identifier,
                    Parameters = { a.Trigger.Parameters.Select(p => new Contracts.AutomationTriggerParameter { Identifier = p.Identifier, Value = p.Value }) },
                    Dependencies = { a.Trigger.Dependencies.Select(p => p.Value.ToString()) }
                }
            });
    }
}
