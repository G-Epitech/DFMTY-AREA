using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationLinkRequestWriteRepository
{
    public Task AddRequestAsync(IntegrationLinkRequest request);
    public Task UpdateRequestAsync(IntegrationLinkRequest request);
    public Task DeleteRequestAsync(IntegrationLinkRequest request);
}
