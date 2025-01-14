using MediatR;

using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationsByAutomationIds;

using Integration = Common.Domain.Integrations.IntegrationAggregate.Integration;

public record GetIntegrationsByAutomationIdsQuery(
    IReadOnlyList<AutomationId> AutomationIds,
    AutomationIntegrationSource Source = AutomationIntegrationSource.Any) : IRequest<Page<Integration>>;
