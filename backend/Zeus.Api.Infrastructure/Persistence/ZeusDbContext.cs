using Microsoft.EntityFrameworkCore;

using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence;

public sealed class ZeusDbContext: DbContext
{
    public DbSet<User> Users { get; init; }
    
    public ZeusDbContext(DbContextOptions<ZeusDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ZeusDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
