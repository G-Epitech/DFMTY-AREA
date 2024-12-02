using Zeus.Api.Domain.IntegrationAggregate;
using Zeus.Api.Domain.IntegrationAggregate.ValueObjects;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationReadRepository
{
    public Task<Integration?> GetIntegrationByIdAsync(IntegrationId id);
}
