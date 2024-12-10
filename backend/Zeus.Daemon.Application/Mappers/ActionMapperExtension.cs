using Zeus.Daemon.Application.Discord.Actions;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;

namespace Zeus.Daemon.Application.Mappers;

public static class ActionMapperExtension
{
    public static Type GetHandler(this AutomationAction action)
    {
        return action.Identifier switch
        {
            "Discord.SendMessageToChannel" => typeof(DiscordSendMessageActionHandler),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
