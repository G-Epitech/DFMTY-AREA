using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Runner.Runner;

namespace Zeus.Daemon.Runner.Builder;

public class DaemonRunnerBuilder
{
    private string[] Args { get; set; } = [];

    public IServiceCollection Services { get; } = new ServiceCollection();

    public IConfigurationManager Configuration { get; private set; }

    private DaemonRunnerBuilder()
    {
        Configuration = BuildConfiguration();
    }

    public static DaemonRunnerBuilder CreateBuilder(string[] args)
    {
        return new DaemonRunnerBuilder { Args = args };
    }

    private static ConfigurationManager BuildConfiguration()
    {
        var configurationManager = new ConfigurationManager();

        configurationManager.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return configurationManager;
    }

    public DaemonRunner Build()
    {
        return new DaemonRunner(Services.BuildServiceProvider(), Configuration.Build());
    }
}
