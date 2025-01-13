namespace Zeus.Daemon.Application.Interfaces;

public interface IDaemonService
{
    public Task StartAsync(CancellationToken cancellationToken);
    public Task StopAsync();
}
