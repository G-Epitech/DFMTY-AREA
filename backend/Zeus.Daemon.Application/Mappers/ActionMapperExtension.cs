using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Daemon.Application.Discord.ActionsHandlers;

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
