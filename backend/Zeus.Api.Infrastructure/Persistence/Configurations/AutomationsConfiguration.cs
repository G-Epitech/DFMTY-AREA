using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.AutomationAggregate.Entities;
using Zeus.Api.Domain.AutomationAggregate.ValueObjects;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Configurations;

public sealed class AutomationsConfiguration : IEntityTypeConfiguration<Automation>
{
    private const int AutomationLabelMaxLength = 100;
    private const int AutomationDescriptionMaxLength = 255;
    private const int ParameterIdentifierMaxLength = 300;

    public void Configure(EntityTypeBuilder<Automation> builder)
    {
        builder.ToTable("Automations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(x => x.Value, x => new AutomationId(x));
        builder.Property(x => x.Label)
            .HasMaxLength(AutomationLabelMaxLength)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(AutomationDescriptionMaxLength);
        builder.Property(x => x.Enabled);
        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        ConfigureAutomationTriggersTable(builder);
        ConfigureAutomationActionsTable(builder);

        builder.Metadata.FindNavigation(nameof(Automation.Actions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Automation.Trigger))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureAutomationActionsTable(EntityTypeBuilder<Automation> builder)
    {
        builder.OwnsMany(x => x.Actions, actions =>
        {
            actions.ToTable("AutomationActions");
            actions.WithOwner().HasForeignKey("AutomationId");
            actions.HasKey("Id");
            actions.Property(x => x.Id)
                .HasColumnName("ActionId")
                .ValueGeneratedNever()
                .HasConversion(x => x.Value, x => new AutomationActionId(x));
            actions.Property(x => x.Identifier)
                .HasMaxLength(ParameterIdentifierMaxLength)
                .IsRequired();
            ConfigureAutomationActionParametersTable(actions);
            ConfigureAutomationActionProvidersTable(actions);
        });
    }

    private static void ConfigureAutomationActionParametersTable(OwnedNavigationBuilder<Automation, AutomationAction> actions)
    {
        actions.OwnsMany(x => x.Parameters, parameters =>
        {
            parameters.ToTable("AutomationActionParameters");
            parameters.WithOwner().HasForeignKey("ActionId");
            parameters.HasKey("ActionId", "Identifier");
            parameters.Property(x => x.Identifier)
                .HasMaxLength(ParameterIdentifierMaxLength)
                .IsRequired();
            parameters.Property(x => x.Value)
                .IsRequired();
        });
    }

    private static void ConfigureAutomationActionProvidersTable(OwnedNavigationBuilder<Automation, AutomationAction> actions)
    {
        actions.OwnsMany(x => x.Providers, providers =>
        {
            providers.ToTable("AutomationActionProviders");
            providers.WithOwner().HasForeignKey("ActionId");
            providers.Property<IntegrationId>("ProviderId")
                .HasConversion(x => x.Value, x => new IntegrationId(x))
                .IsRequired();
            providers.HasKey("ActionId", "ProviderId");
        });
    }

    private static void ConfigureAutomationTriggersTable(EntityTypeBuilder<Automation> builder)
    {
        builder.OwnsOne(x => x.Trigger, trigger =>
        {
            trigger.ToTable("AutomationTriggers");
            trigger.WithOwner().HasForeignKey("AutomationId");
            trigger.HasKey("Id");
            trigger.Property(x => x.Id)
                .HasColumnName("TriggerId")
                .ValueGeneratedNever()
                .HasConversion(x => x.Value, x => new AutomationTriggerId(x));
            trigger.Property(x => x.Identifier)
                .HasMaxLength(ParameterIdentifierMaxLength)
                .IsRequired();
            ConfigureAutomationTriggerParametersTable(trigger);
            ConfigureAutomationTriggerProvidersTable(trigger);
        });
    }

    private static void ConfigureAutomationTriggerProvidersTable(OwnedNavigationBuilder<Automation, AutomationTrigger> trigger)
    {
        trigger.OwnsMany(x => x.Providers, providers =>
        {
            providers.ToTable("AutomationTriggerProviders");
            providers.WithOwner().HasForeignKey("TriggerId");
            providers.HasKey("TriggerId");
            providers.Property<IntegrationId>("ProviderId")
                .HasConversion(x => x.Value, x => new IntegrationId(x))
                .IsRequired();
        });
        trigger.Navigation(x => x.Providers).Metadata.SetField("_providers");
        trigger.Navigation(x => x.Providers).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureAutomationTriggerParametersTable(OwnedNavigationBuilder<Automation, AutomationTrigger> trigger)
    {
        trigger.OwnsMany(x => x.Parameters, parameters =>
        {
            parameters.ToTable("AutomationTriggerParameters");
            parameters.WithOwner().HasForeignKey("TriggerId");
            parameters.HasKey("TriggerId", "Identifier");
            parameters.Property(x => x.Identifier)
                .HasMaxLength(ParameterIdentifierMaxLength)
                .IsRequired();
            parameters.Property(x => x.Value)
                .IsRequired();
        });
    }
}
