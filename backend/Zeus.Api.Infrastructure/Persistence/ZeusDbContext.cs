using Microsoft.EntityFrameworkCore;

using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence;

public sealed class ZeusDbContext : DbContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<Integration> Integrations { get; init; }
    public DbSet<IntegrationLinkRequest> IntegrationLinkRequests { get; init; }
    public DbSet<Automation> Automations { get; init; }

    public ZeusDbContext(DbContextOptions<ZeusDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZeusDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
