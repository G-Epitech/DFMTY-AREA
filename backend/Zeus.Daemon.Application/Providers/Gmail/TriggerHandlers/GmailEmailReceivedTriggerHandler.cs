using Zeus.Daemon.Application.Attributes;

namespace Zeus.Daemon.Application.Providers.Gmail.TriggerHandlers;

[TriggerHandler("Gmail.EmailReceived")]
public class GmailEmailReceivedTriggerHandler
{
    [OnTriggerRegister]
    public Task<bool> OnRegisterAsync()
    {
        return Task.FromResult(true);
    }

    [OnTriggerRemove]
    public Task<bool> OnRemoveAsync()
    {
        return Task.FromResult(true);
    }
}
