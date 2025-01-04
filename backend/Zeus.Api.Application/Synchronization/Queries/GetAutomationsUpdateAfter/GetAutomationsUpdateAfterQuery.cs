using MediatR;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.Application.Synchronization.Queries.GetAutomationsUpdateAfter;

public sealed record GetAutomationsUpdateAfterQuery(
    AutomationState State,
    DateTime LastUpdate
) : IRequest<List<Automation>>;
