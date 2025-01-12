using ErrorOr;

using Zeus.Api.Domain.Integrations.Properties;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

using Integration = Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration;

public interface IIntegrationService
{
    public Task<ErrorOr<IntegrationProperties>> GetProperties(Integration integration);
}
