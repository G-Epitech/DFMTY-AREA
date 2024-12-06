﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Zeus.Api.Domain.Integrations.Common.Enums;
using Zeus.Api.Infrastructure.Persistence;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    [DbContext(typeof(ZeusDbContext))]
    [Migration("20241207110431_AddIntegrations")]
    partial class AddIntegrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "IntegrationType", new[] { "discord", "gmail" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Zeus.Api.Domain.Integrations.IntegrationAggregate.Integration", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<IntegrationType>("Type")
                        .HasColumnType("\"IntegrationType\"");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Integrations", (string)null);

                    b.HasDiscriminator<IntegrationType>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Zeus.Api.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Zeus.Api.Domain.Integrations.IntegrationAggregate.DiscordIntegration", b =>
                {
                    b.HasBaseType("Zeus.Api.Domain.Integrations.IntegrationAggregate.Integration");

                    b.HasDiscriminator().HasValue(IntegrationType.Discord);
                });

            modelBuilder.Entity("Zeus.Api.Domain.Integrations.IntegrationAggregate.GmailIntegration", b =>
                {
                    b.HasBaseType("Zeus.Api.Domain.Integrations.IntegrationAggregate.Integration");

                    b.HasDiscriminator().HasValue(IntegrationType.Gmail);
                });

            modelBuilder.Entity("Zeus.Api.Domain.Integrations.IntegrationAggregate.Integration", b =>
                {
                    b.OwnsMany("Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects.IntegrationToken", "Tokens", b1 =>
                        {
                            b1.Property<string>("Value")
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("TokenValue");

                            b1.Property<string>("Type")
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("TokenType");

                            b1.Property<int>("Usage")
                                .HasColumnType("integer")
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
