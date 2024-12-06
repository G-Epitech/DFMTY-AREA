using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Repositories;

public static class InMemoryStore
{
    public static List<Integration> Integrations { get; } = [];
    public static List<IntegrationLinkRequest> IntegrationLinkRequests { get; } = [];
}
