using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationLinkRequestReadRepository
{
    public Task<IntegrationLinkRequest?> GetRequestByIdAsync(
        IntegrationLinkRequestId id,
        CancellationToken cancellationToken = default);
}
