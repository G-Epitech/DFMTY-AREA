using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Runner.Runner;

namespace Zeus.Daemon.Runner.Builder;

public class DaemonRunnerBuilder
{
    private string[] Args { get; set; } = [];
    
    public IServiceCollection Services { get; } = new ServiceCollection();

    private DaemonRunnerBuilder() {}

    public static DaemonRunnerBuilder CreateBuilder(string[] args)
    {
        return new DaemonRunnerBuilder { Args = args };
    }

    public DaemonRunner Build()
    {
        return new DaemonRunner(Services.BuildServiceProvider());
    }
}
