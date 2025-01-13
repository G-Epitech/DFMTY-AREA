using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Configurations;

using Integration = Common.Domain.Integrations.IntegrationAggregate.Integration;

public class IntegrationsConfiguration : IEntityTypeConfiguration<Integration>
{
    private const int ClientIdMaxLength = 255;
    private const int TokenValueMaxLength = 255;
    private const int TokenTypeMaxLength = 100;

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
            .HasValue<NotionIntegration>(IntegrationType.Notion)
            .HasValue<GmailIntegration>(IntegrationType.Gmail);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, v => new IntegrationId(v));
        builder.Property(i => i.ClientId)
            .HasMaxLength(ClientIdMaxLength);
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

    private static void ConfigureIntegrationTokensTable(EntityTypeBuilder<Integration> builder)
    {
        builder.OwnsMany(i => i.Tokens, tb =>
        {
            tb.ToTable("IntegrationTokens");
            tb.WithOwner().HasForeignKey("IntegrationId");
            tb.HasKey("Value", "Type", "Usage");
            tb.Property(t => t.Value)
                .HasColumnName("TokenValue")
                .HasMaxLength(TokenValueMaxLength);
            tb.Property(t => t.Type)
                .HasColumnName("TokenType")
                .HasMaxLength(TokenTypeMaxLength);
            tb.Property(t => t.Usage)
                .HasColumnName("TokenUsage");
        });
        builder.Metadata.FindNavigation(nameof(Integration.Tokens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
