using MediatR;

using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Synchronization.Queries.GetAutomationsLastUpdate;

public sealed record GetAutomationsLastUpdateQuery(
    AutomationState State,
    UserId? OwnerId = null
) : IRequest<DateTime?>;
