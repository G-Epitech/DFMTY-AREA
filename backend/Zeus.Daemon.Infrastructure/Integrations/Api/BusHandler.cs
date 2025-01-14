using MassTransit;

using Zeus.Common.Extensions.DependencyInjection;

namespace Zeus.Daemon.Infrastructure.Integrations.Api;

[AutoStarted]
public class BusHandler
{
    public BusHandler(IBusControl busControl)
    {
        busControl.StartAsync().GetAwaiter().GetResult();
    }
}
