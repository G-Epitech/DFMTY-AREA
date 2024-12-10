using ErrorOr;

using MediatR;

using Zeus.Api.Domain.AutomationAggregate;

namespace Zeus.Api.Application.Automations.Query.GetAutomation;

public record GetAutomationQuery(
    Guid UserId,
    Guid AutomationId) : IRequest<ErrorOr<Automation>>;