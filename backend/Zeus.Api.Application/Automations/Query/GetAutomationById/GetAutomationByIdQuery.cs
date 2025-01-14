using ErrorOr;

using MediatR;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;

namespace Zeus.Api.Application.Automations.Query.GetAutomationById;

public record GetAutomationByIdQuery(
    AutomationId AutomationId) : IRequest<ErrorOr<Automation>>;
