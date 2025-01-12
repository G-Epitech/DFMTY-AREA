namespace Zeus.Api.Application.Interfaces.Repositories;

using Integration = Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration;

public interface IIntegrationWriteRepository
{
    public Task AddIntegrationAsync(Integration integration,
        CancellationToken cancellationToken = default);

    public Task UpdateIntegrationAsync(Integration integration,
        CancellationToken cancellationToken = default);

    public Task DeleteIntegrationAsync(Integration integration,
        CancellationToken cancellationToken = default);
}
