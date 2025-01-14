using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Extensions.Queryable;

namespace Zeus.Api.Application.Interfaces.Repositories;

using Integration = Common.Domain.Integrations.IntegrationAggregate.Integration;

public interface IIntegrationReadRepository
{
    public Task<Integration?> GetIntegrationByIdAsync(IntegrationId id,
        CancellationToken cancellationToken = default);

    public Task<Page<Integration>> GetIntegrationsAsync(PageQuery query,
        CancellationToken cancellationToken = default);

    public Task<Page<Integration>> GetIntegrationsByOwnerIdAsync(UserId userId, PageQuery query,
        CancellationToken cancellationToken = default);

    public Task<Page<Integration>> GetIntegrationsByAutomationIdsAsync(
        IReadOnlyList<AutomationId> automationIds,
        AutomationIntegrationSource source = AutomationIntegrationSource.Any,
        CancellationToken cancellationToken = default);
}
