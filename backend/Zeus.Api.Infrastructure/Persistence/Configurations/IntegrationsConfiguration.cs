using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zeus.Api.Domain.Integrations.Common.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Infrastructure.Persistence.Configurations;

public class IntegrationsConfiguration : IEntityTypeConfiguration<Integration>
{
    public void Configure(EntityTypeBuilder<Integration> builder)
    {
        ConfigureIntegrationsTable(builder);
        ConfigureIntegrationTokensTable(builder);
    }

    private static void ConfigureIntegrationsTable(EntityTypeBuilder<Integration> builder)
    {
        builder.ToTable("Integrations");
        builder.HasDiscriminator(i => i.Type)
            .HasValue<DiscordIntegration>(IntegrationType.Discord)
            .HasValue<GmailIntegration>(IntegrationType.Gmail);
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => i.OwnerId);
        builder.Property(i => i.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, v => new IntegrationId(v));
        builder.Property(i => i.OwnerId)
            .HasConversion(id => id.Value, v => new UserId(v));
        builder.Property(i => i.ClientId)
            .HasMaxLength(255);
        builder.Property(i => i.Type);
        builder.Ignore(i => i.Tokens);
    }
    
    private static void ConfigureIntegrationTokensTable(EntityTypeBuilder<Integration> builder)
    {
        builder.OwnsMany(i => i.Tokens, tb =>
        {
            tb.ToTable("IntegrationTokens");
            tb.WithOwner().HasForeignKey("IntegrationId");
            tb.HasKey("Value", "Type", "Usage");
            tb.Property(t => t.Value)
                .HasColumnName("TokenValue")
                .HasMaxLength(255);
            tb.Property(t => t.Type)
                .HasColumnName("TokenType")
                .HasMaxLength(255);
            tb.Property(t => t.Usage)
                .HasColumnName("TokenUsage");
        });
        builder.Metadata.FindNavigation(nameof(Integration.Tokens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
