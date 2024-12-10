using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Configurations;

public class IntegrationLinkRequestConfiguration : IEntityTypeConfiguration<IntegrationLinkRequest>
{
    public void Configure(EntityTypeBuilder<IntegrationLinkRequest> builder)
    {
        builder.ToTable("IntegrationLinkRequests");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedNever()
            .HasConversion(i => i.Value, v => new IntegrationLinkRequestId(v));
        builder.Property(i => i.Type);
        builder.Property(x => x.CreatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(i => i.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
