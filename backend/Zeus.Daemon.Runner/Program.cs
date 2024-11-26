using Zeus.Daemon.Runner.Builder;

var builder = DaemonRunnerBuilder.CreateBuilder(args);

var runner = builder.Build();

runner.Run();
