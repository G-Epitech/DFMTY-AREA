using Microsoft.Extensions.DependencyInjection;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Discord.Triggers;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Infrastructure.Automations;

public class AutomationHandlersRegistry : IAutomationHandlersRegistry
{
    private class CancelableTask
    {
        public Task Task { get; set; }
        private CancellationTokenSource CancellationTokenSource { get; set; }

        public CancelableTask(Func<CancellationToken, Task> task)
        {
            CancellationTokenSource = new CancellationTokenSource();
            Task = task(CancellationTokenSource.Token);
        }

        public void Cancel()
        {
            CancellationTokenSource.Cancel();
        }
    }

    private readonly Dictionary<AutomationId, ITriggerHandler> _handlers = [];
    private readonly Dictionary<AutomationId, CancelableTask> _tasks = [];
    private readonly IServiceProvider _serviceProvider;

    public AutomationHandlersRegistry(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task RegisterAutomationAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        if (_handlers.ContainsKey(automation.Id))
        {
            return Task.CompletedTask;
        }

        var handler = CreateTriggerHandler(automation.Trigger);

        if (handler == null)
        {
            Console.WriteLine($"Handler for trigger '{automation.Trigger.Identifier}' not found. Skipped.");
            return Task.CompletedTask;
        }

        var context = new AutomationExecutionContext(automation, []);

        _handlers.Add(automation.Id, handler);
        _tasks[automation.Id] = new CancelableTask(token => handler.HandleAsync(context, token));
        
        Console.WriteLine($"Registered automation '{automation.Id.Value.ToString()}'.");
        return Task.CompletedTask;
    }

    private async Task CancelAutomationAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        if (_handlers.TryGetValue(automationId, out var handler))
        {
            await handler.CancelAsync(cancellationToken);
        }
        if (_tasks.TryGetValue(automationId, out var task))
        {
            task.Cancel();
        }

        _handlers.Remove(automationId);
        _tasks.Remove(automationId);
        Console.WriteLine($"Cancelled automation '{automationId.Value.ToString()}'.");
    }

    public async Task RefreshAutomationsAsync(IEnumerable<Automation> automations, CancellationToken cancellationToken = default)
    {
        foreach (var automation in automations)
        {
            var handler = _handlers.GetValueOrDefault(automation.Id);

            if (automation.Enabled)
            {
                var task = handler == null
                    ? RegisterAutomationAsync(automation, cancellationToken)
                    : RecreateAutomationContextAsync(automation);

                await task;
            }
            else
            {
                await CancelAutomationAsync(automation.Id, cancellationToken);
            }
        }
    }

    private async Task RecreateAutomationContextAsync(Automation automation)
    {
        await CancelAutomationAsync(automation.Id);
        await RegisterAutomationAsync(automation);
    }

    public async Task RemoveAutomationAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        await CancelAutomationAsync(automationId, cancellationToken);
    }

    private ITriggerHandler? CreateTriggerHandler(AutomationTrigger trigger)
    {
        return trigger.Identifier switch
        {
            "Discord.MessageReceivedInChannel" => ActivatorUtilities.CreateInstance<DiscordMessageReceivedTriggerHandler>(_serviceProvider),
            _ => null
        };
    }
}
