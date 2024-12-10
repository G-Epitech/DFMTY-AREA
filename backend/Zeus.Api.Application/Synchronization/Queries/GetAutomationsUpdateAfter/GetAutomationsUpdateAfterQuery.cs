using MediatR;

using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.Application.Synchronization.Queries.GetAutomationsUpdateAfter;

public sealed record GetAutomationsUpdateAfterQuery(
    AutomationState State,
    DateTime LastUpdate
) : IRequest<List<Automation>>;
