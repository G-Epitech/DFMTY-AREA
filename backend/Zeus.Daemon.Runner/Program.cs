﻿using Zeus.Daemon.Application;
using Zeus.Daemon.Infrastructure;
using Zeus.Daemon.Runner.Builder;

var builder = DaemonRunnerBuilder.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
}

var runner = builder.Build();
{
    runner.Run();
}
