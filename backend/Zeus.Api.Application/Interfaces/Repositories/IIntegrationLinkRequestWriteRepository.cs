using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationLinkRequestWriteRepository
{
    public Task AddRequestAsync(IntegrationLinkRequest request,
        CancellationToken cancellationToken = default);

    public Task UpdateRequestAsync(IntegrationLinkRequest request,
        CancellationToken cancellationToken = default);

    public Task DeleteRequestAsync(IntegrationLinkRequest request,
        CancellationToken cancellationToken = default);
}
