﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.Enums;
using Zeus.Api.Infrastructure.Persistence;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    [DbContext(typeof(ZeusDbContext))]
    [Migration("20250114163404_AddLeagueOfLegendsIntegration")]
    partial class AddLeagueOfLegendsIntegration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "AuthenticationMethodType", new[] { "google", "password" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "AutomationActionParameterType", new[] { "raw", "var" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "IntegrationTokenUsage", new[] { "access", "refresh" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "IntegrationType", new[] { "discord", "gmail", "league_of_legends", "notion", "open_ai" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.AuthenticationMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<AuthenticationMethodType>("Type")
                        .HasColumnType("\"AuthenticationMethodType\"");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuthenticationMethods", (string)null);

                    b.HasDiscriminator<AuthenticationMethodType>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.IntegrationLinkRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<IntegrationType>("Type")
                        .HasColumnType("\"IntegrationType\"");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("IntegrationLinkRequests", (string)null);
                });

            modelBuilder.Entity("Zeus.Common.Domain.AutomationAggregate.Automation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Automations", (string)null);
                });

            modelBuilder.Entity("Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<IntegrationType>("Type")
                        .HasColumnType("\"IntegrationType\"");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Integrations", (string)null);

                    b.HasDiscriminator<IntegrationType>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Zeus.Common.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.GoogleAuthenticationMethod", b =>
                {
                    b.HasBaseType("Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.AuthenticationMethod");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProviderUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(AuthenticationMethodType.Google);
                });

            modelBuilder.Entity("Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.PasswordAuthenticationMethod", b =>
                {
                    b.HasBaseType("Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.AuthenticationMethod");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(AuthenticationMethodType.Password);
                });

            modelBuilder.Entity("Zeus.Common.Domain.Integrations.IntegrationAggregate.DiscordIntegration", b =>
                {
                    b.HasBaseType("Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration");

                    b.HasDiscriminator().HasValue(IntegrationType.Discord);
                });

            modelBuilder.Entity("Zeus.Common.Domain.Integrations.IntegrationAggregate.GmailIntegration", b =>
                {
                    b.HasBaseType("Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration");

                    b.HasDiscriminator().HasValue(IntegrationType.Gmail);
                });

            modelBuilder.Entity("Zeus.Common.Domain.Integrations.IntegrationAggregate.LeagueOfLegendsIntegration", b =>
                {
                    b.HasBaseType("Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration");

                    b.HasDiscriminator().HasValue(IntegrationType.LeagueOfLegends);
                });

            modelBuilder.Entity("Zeus.Common.Domain.Integrations.IntegrationAggregate.NotionIntegration", b =>
                {
                    b.HasBaseType("Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration");

                    b.HasDiscriminator().HasValue(IntegrationType.Notion);
                });

            modelBuilder.Entity("Zeus.Common.Domain.Integrations.IntegrationAggregate.OpenAiIntegration", b =>
                {
                    b.HasBaseType("Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration");

                    b.HasDiscriminator().HasValue(IntegrationType.OpenAi);
                });

            modelBuilder.Entity("Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.AuthenticationMethod", b =>
                {
                    b.HasOne("Zeus.Common.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.IntegrationLinkRequest", b =>
                {
                    b.HasOne("Zeus.Common.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Zeus.Common.Domain.AutomationAggregate.Automation", b =>
                {
                    b.HasOne("Zeus.Common.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Zeus.Common.Domain.AutomationAggregate.Entities.AutomationAction", "Actions", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("ActionId");

                            b1.Property<Guid>("AutomationId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Identifier")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.Property<int>("Rank")
                                .HasColumnType("integer");

                            b1.HasKey("Id");

                            b1.HasIndex("AutomationId");

                            b1.ToTable("AutomationActions", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("AutomationId");

                            b1.OwnsMany("Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects.IntegrationId", "Providers", b2 =>
                                {
                                    b2.Property<Guid>("ActionId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("ProviderId")
                                        .HasColumnType("uuid");

                                    b2.HasKey("ActionId", "ProviderId");

                                    b2.ToTable("AutomationActionProviders", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("ActionId");
                                });

                            b1.OwnsMany("Zeus.Common.Domain.AutomationAggregate.ValueObjects.AutomationActionParameter", "Parameters", b2 =>
                                {
                                    b2.Property<Guid>("ActionId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Identifier")
                                        .HasMaxLength(300)
                                        .HasColumnType("character varying(300)");

                                    b2.Property<AutomationActionParameterType>("Type")
                                        .HasColumnType("\"AutomationActionParameterType\"");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ActionId", "Identifier");

                                    b2.ToTable("AutomationActionParameters", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("ActionId");
                                });

                            b1.Navigation("Parameters");

                            b1.Navigation("Providers");
                        });

                    b.OwnsOne("Zeus.Common.Domain.AutomationAggregate.Entities.AutomationTrigger", "Trigger", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("TriggerId");

                            b1.Property<Guid>("AutomationId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Identifier")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.HasKey("Id");

                            b1.HasIndex("AutomationId")
                                .IsUnique();

                            b1.ToTable("AutomationTriggers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("AutomationId");

                            b1.OwnsMany("Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects.IntegrationId", "Providers", b2 =>
                                {
                                    b2.Property<Guid>("TriggerId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("ProviderId")
                                        .HasColumnType("uuid");

                                    b2.HasKey("TriggerId");

                                    b2.ToTable("AutomationTriggerProviders", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("TriggerId");
                                });

                            b1.OwnsMany("Zeus.Common.Domain.AutomationAggregate.ValueObjects.AutomationTriggerParameter", "Parameters", b2 =>
                                {
                                    b2.Property<Guid>("TriggerId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Identifier")
                                        .HasMaxLength(300)
                                        .HasColumnType("character varying(300)");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("TriggerId", "Identifier");

                                    b2.ToTable("AutomationTriggerParameters", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("TriggerId");
                                });

                            b1.Navigation("Parameters");

                            b1.Navigation("Providers");
                        });

                    b.Navigation("Actions");

                    b.Navigation("Trigger")
                        .IsRequired();
                });

            modelBuilder.Entity("Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration", b =>
                {
                    b.HasOne("Zeus.Common.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects.IntegrationToken", "Tokens", b1 =>
                        {
                            b1.Property<string>("Value")
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("TokenValue");

                            b1.Property<string>("Type")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("TokenType");

                            b1.Property<IntegrationTokenUsage>("Usage")
                                .HasColumnType("\"IntegrationTokenUsage\"")
                                .HasColumnName("TokenUsage");

                            b1.Property<Guid>("IntegrationId")
                                .HasColumnType("uuid");

                            b1.HasKey("Value", "Type", "Usage");

                            b1.HasIndex("IntegrationId");

                            b1.ToTable("IntegrationTokens", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("IntegrationId");
                        });

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
