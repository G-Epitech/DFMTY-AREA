using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Zeus.Api.Infrastructure;
using Zeus.Api.Infrastructure.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructure(context.Configuration);
    })
    .Build();

using var scope = host.Services.CreateScope();

{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ZeusDbContext>();
        Console.WriteLine("Applying migrations...");
        await context.Database.MigrateAsync();
        Console.WriteLine("Done.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
        Environment.Exit(1);
    }
}
