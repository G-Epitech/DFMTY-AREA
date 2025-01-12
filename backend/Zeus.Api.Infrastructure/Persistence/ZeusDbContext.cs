using Microsoft.EntityFrameworkCore;

using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Infrastructure.Persistence.Interceptors;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence;

using Integration = Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration;

public sealed class ZeusDbContext : DbContext
{
    private readonly AuditableEntitiesInterceptor _auditableEntitiesInterceptor;
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public ZeusDbContext(
        DbContextOptions<ZeusDbContext> options,
        AuditableEntitiesInterceptor auditableEntitiesInterceptor,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor)
        : base(options)
    {
        _auditableEntitiesInterceptor = auditableEntitiesInterceptor;
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<User> Users { get; init; }
    public DbSet<Integration> Integrations { get; init; }
    public DbSet<IntegrationLinkRequest> IntegrationLinkRequests { get; init; }
    public DbSet<Automation> Automations { get; init; }
    public DbSet<AuthenticationMethod> AuthenticationMethods { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZeusDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitiesInterceptor);
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
