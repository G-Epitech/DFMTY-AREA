using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Api.Application.Automations.Commands.CreateAutomation;

public class CreateAutomationCommandHandler : IRequestHandler<CreateAutomationCommand, ErrorOr<Automation>>
{
    private readonly IAutomationWriteRepository _automationWriteRepository;

    public CreateAutomationCommandHandler(IAutomationWriteRepository automationWriteRepository)
    {
        _automationWriteRepository = automationWriteRepository;
    }

    public async Task<ErrorOr<Automation>> Handle(CreateAutomationCommand command, CancellationToken cancellationToken)
    {
        var trigger = CreateTrigger(command.Trigger);
        var actions = command.Actions.Select(CreateAction).ToList();

        var automation = Automation.Create(
            command.Label,
            command.Description,
            command.OwnerId,
            trigger,
            actions,
            command.Enabled
        );

        await _automationWriteRepository.AddAutomationAsync(automation, cancellationToken);

        return automation;
    }

    private static AutomationTrigger CreateTrigger(CreateAutomationTriggerCommand trigger)
    {
        var parameters = trigger.Parameters.Select(p => new AutomationTriggerParameter { Identifier = p.Identifier, Value = p.Value }).ToList();
        var providers = trigger.Providers.Select(p => new IntegrationId(p)).ToList();

        return AutomationTrigger.Create(trigger.Identifier, parameters, providers);
    }

    private static AutomationAction CreateAction(CreateAutomationActionCommand action, int rank)
    {
        var parameters = action.Parameters.Select(p => new AutomationActionParameter { Identifier = p.Identifier, Value = p.Value, Type = p.Type }).ToList();
        var providers = action.Providers.Select(p => new IntegrationId(p)).ToList();

        return AutomationAction.Create(action.Identifier, rank, parameters, providers);
    }
}
