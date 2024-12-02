using Zeus.Api.Domain.IntegrationAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationWriteRepository
{
    public Task AddIntegrationAsync(Integration integration);
    public Task UpdateIntegrationAsync(Integration integration);
    public Task DeleteIntegrationAsync(Integration integration);
}
