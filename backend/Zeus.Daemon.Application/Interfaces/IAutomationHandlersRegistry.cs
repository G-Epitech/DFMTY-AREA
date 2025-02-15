﻿using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;

namespace Zeus.Daemon.Application.Interfaces;

public interface IAutomationHandlersRegistry
{
    public Task RegisterAutomationAsync(Automation automation, CancellationToken cancellationToken = default);
    public Task RemoveAutomationAsync(AutomationId automationId, CancellationToken cancellationToken = default);
    public Task RefreshAutomationsAsync(IEnumerable<Automation> automations, CancellationToken cancellationToken = default);
}
