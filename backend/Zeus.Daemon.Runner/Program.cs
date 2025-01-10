using Microsoft.Extensions.Configuration;

using Zeus.Common.Domain.ProvidersSettings;
using Zeus.Daemon.Application;
using Zeus.Daemon.Infrastructure;
using Zeus.Daemon.Runner.Builder;

var builder = DaemonRunnerBuilder.CreateBuilder(args);
{
    #region Configuration

    builder.Configuration.AddUserSecrets<Program>();

    #endregion Configuration

    #region Services

    await builder.Services.AddProvidersSettingsAsync();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    #endregion Services
}

var runner = builder.Build();
{
    await runner.Run();
}
