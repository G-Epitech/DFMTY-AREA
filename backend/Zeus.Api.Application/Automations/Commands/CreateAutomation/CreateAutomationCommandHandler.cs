using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

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
        var triggerParams = new List<AutomationTriggerParameter>
        {
            new() { Identifier = "GuildId", Value = "1316046870178697267" },
            new() { Identifier = "ChannelId", Value = "1316046972733620244" }
        };

        var trigger =
            AutomationTrigger.Create("Discord.MessageReceivedInChannel", triggerParams, []);

        var actionParams = new List<AutomationActionParameter>
        {
            new() { Identifier = "ChannelId", Value = "1316046972733620244", Type = AutomationActionParameterType.Raw },
            new()
            {
                Identifier = "Content",
                Value =
                    ":man_facepalming: Ma tête en voyant ce message\nhttps://media.discordapp.net/attachments/1298924031394971739/1298928041988198420/IMG_4880_1.gif",
                Type = AutomationActionParameterType.Raw
            }
        };

        var action = AutomationAction.Create("Discord.SendMessageToChannel", 0, actionParams, []);

        var automation = Automation.Create(
            "Reply with a Yann gif",
            "Reply to any message with a unique gif where yann is present",
            new UserId(command.UserId),
            trigger, [action], true);

        await _automationWriteRepository.AddAutomationAsync(automation, cancellationToken);

        return automation;
    }
}
