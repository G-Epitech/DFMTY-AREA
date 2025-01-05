using ErrorOr;

using Zeus.Api.Domain.Integrations.Properties;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;

namespace Zeus.Api.Application.Interfaces.Services.Integrations;

public interface IIntegrationService
{
    public Task<ErrorOr<IntegrationProperties>> GetProperties(Integration integration);
}
