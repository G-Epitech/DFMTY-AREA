namespace Zeus.Daemon.Runner.Runner;


public class DaemonRunner
{
    private readonly IServiceProvider _serviceProvider;
    
    public DaemonRunner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    /**
     * To use dependency injection in a class that is not registered in the service collection,
     * you can use the ActivatorUtilities class, like so:
     * ActivatorUtilities.CreateInstance<IService>(_serviceProvider);
     */
    public void Run()
    {
        Console.WriteLine("Running daemon...");
    }
}
