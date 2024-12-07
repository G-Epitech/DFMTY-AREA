﻿using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

namespace Zeus.Api.Application.Interfaces.Repositories;

public interface IIntegrationWriteRepository
{
    public Task AddIntegrationAsync(Integration integration);
    public Task UpdateIntegrationAsync(Integration integration);
    public Task DeleteIntegrationAsync(Integration integration);
}
