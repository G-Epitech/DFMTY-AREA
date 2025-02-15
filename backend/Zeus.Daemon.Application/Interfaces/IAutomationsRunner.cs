﻿using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Interfaces;

public interface IAutomationsRunner
{
    public Task<bool> RunAsync(AutomationId automationId, FactsDictionary facts);

    public Task<Dictionary<AutomationId, bool>> RunManyAsync(IReadOnlyList<AutomationId> automationIds, FactsDictionary facts);
}
