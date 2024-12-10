using MediatR;

using Zeus.Api.Domain.AutomationAggregate.Enums;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Synchronization.Queries;

public sealed record GetAutomationsLastUpdateQuery(
    AutomationState State,
    UserId? OwnerId = null
) : IRequest<DateTime>;
