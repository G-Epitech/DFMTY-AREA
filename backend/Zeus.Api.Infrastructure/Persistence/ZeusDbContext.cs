using Microsoft.EntityFrameworkCore;

using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Infrastructure.Persistence.Interceptors;
using Zeus.Api.Infrastructure.Services;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence;

using Integration = Common.Domain.Integrations.IntegrationAggregate.Integration;

public sealed class ZeusDbContext : DbContext
{
    private readonly AuditableEntitiesInterceptor _auditableEntitiesInterceptor;
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    private readonly DomainEventDelayer _eventDelayer;

    public ZeusDbContext(
        DbContextOptions<ZeusDbContext> options,
        AuditableEntitiesInterceptor auditableEntitiesInterceptor,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor,
        DomainEventDelayer eventDelayer)
        : base(options)
    {
        _auditableEntitiesInterceptor = auditableEntitiesInterceptor;
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        _eventDelayer = eventDelayer;
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await _eventDelayer.PublishDelayedEventsAsync(cancellationToken);
        return result;
    }
}
