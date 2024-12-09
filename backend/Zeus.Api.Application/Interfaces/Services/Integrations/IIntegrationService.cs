using ErrorOr;

using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.Properties;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IIntegrationService
{
    public Task<ErrorOr<IntegrationProperties>> GetProperties(Integration integration);
}
