using Zeus.Api.Domain.Integrations.IntegrationAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationWriteRepository
{
    public Task AddIntegrationAsync(Integration integration,
        CancellationToken cancellationToken = default);
    public Task UpdateIntegrationAsync(Integration integration,
        CancellationToken cancellationToken = default);
    public Task DeleteIntegrationAsync(Integration integration,
        CancellationToken cancellationToken = default);
}
