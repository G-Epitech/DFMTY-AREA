using ErrorOr;

using MediatR;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Automations.Query.GetAutomations;

public record GetAutomationsQuery(
    Guid UserId,
    int? Index,
    int? Limit) : IRequest<ErrorOr<Page<Automation>>>;
