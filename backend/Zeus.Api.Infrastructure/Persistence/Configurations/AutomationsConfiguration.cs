using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate;

namespace Zeus.Api.Infrastructure.Persistence.Configurations;
public sealed class AutomationsConfiguration : IEntityTypeConfiguration<Automation>
{
    private const int ParameterIdentifierMaxLength = 300;

    public void Configure(EntityTypeBuilder<Automation> builder)
    {
        builder.ToTable("Automations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(x => x.Value, x => new AutomationId(x));
        builder.Property(x => x.Label)
            .HasMaxLength(Automation.LabelMaxLength)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(Automation.DescriptionMaxLength); builder.Ignore(x => x.Dependencies);
        builder.Property(x => x.Enabled);
        builder.Property(x => x.CreatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedNever()
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
            RelationalPropertyBuilderExtensions.HasColumnName(actions.Property(x => x.Id), "ActionId")
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
        actions.OwnsMany(x => x.Dependencies, provider =>
        {
            provider.ToTable("AutomationActionDependencies");
            provider.WithOwner().HasForeignKey("ActionId");

            provider.Property(x => x.Value)
                .HasColumnName("DependencyId")
                .IsRequired();
            provider.HasKey("ActionId", "Value");
        });
        actions.Navigation(x => x.Dependencies).Metadata.SetField("_dependencies");
        actions.Navigation(x => x.Dependencies).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureAutomationTriggersTable(EntityTypeBuilder<Automation> builder)
    {
        builder.OwnsOne(x => x.Trigger, trigger =>
        {
            trigger.ToTable("AutomationTriggers");
            trigger.WithOwner().HasForeignKey("AutomationId");
            trigger.HasKey("Id");
            RelationalPropertyBuilderExtensions.HasColumnName(trigger.Property(x => x.Id), "TriggerId")
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
        trigger.OwnsMany(x => x.Dependencies, provider =>
        {
            provider.ToTable("AutomationTriggerDependencies");
            provider.WithOwner().HasForeignKey("TriggerId");
            provider.HasKey("TriggerId");

            provider.Property(x => x.Value)
                .HasColumnName("DependencyId")
                .IsRequired();
            provider.HasKey("TriggerId", "Value");
        });
        trigger.Navigation(x => x.Dependencies).Metadata.SetField("_dependencies");
        trigger.Navigation(x => x.Dependencies).UsePropertyAccessMode(PropertyAccessMode.Field);
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
