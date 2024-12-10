using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Zeus.Api.Domain.AutomationAggregate.Enums;
using Zeus.Api.Domain.Integrations.Common.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.Enums;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .Annotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .Annotation("Npgsql:Enum:IntegrationType", "discord,gmail");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Automations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automations_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationLinkRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<IntegrationType>(type: "\"IntegrationType\"", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationLinkRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationLinkRequests_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Integrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<IntegrationType>(type: "\"IntegrationType\"", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrations_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutomationActions",
                columns: table => new
                {
                    ActionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Identifier = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    AutomationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomationActions", x => x.ActionId);
                    table.ForeignKey(
                        name: "FK_AutomationActions_Automations_AutomationId",
                        column: x => x.AutomationId,
                        principalTable: "Automations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutomationTriggers",
                columns: table => new
                {
                    TriggerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Identifier = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    AutomationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomationTriggers", x => x.TriggerId);
                    table.ForeignKey(
                        name: "FK_AutomationTriggers_Automations_AutomationId",
                        column: x => x.AutomationId,
                        principalTable: "Automations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationTokens",
                columns: table => new
                {
                    TokenValue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TokenType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TokenUsage = table.Column<IntegrationTokenUsage>(type: "\"IntegrationTokenUsage\"", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationTokens", x => new { x.TokenValue, x.TokenType, x.TokenUsage });
                    table.ForeignKey(
                        name: "FK_IntegrationTokens_Integrations_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "Integrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutomationActionParameters",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    ActionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<AutomationActionParameterType>(type: "\"AutomationActionParameterType\"", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomationActionParameters", x => new { x.ActionId, x.Identifier });
                    table.ForeignKey(
                        name: "FK_AutomationActionParameters_AutomationActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "AutomationActions",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutomationActionProviders",
                columns: table => new
                {
                    ActionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomationActionProviders", x => new { x.ActionId, x.ProviderId });
                    table.ForeignKey(
                        name: "FK_AutomationActionProviders_AutomationActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "AutomationActions",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutomationTriggerParameters",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    TriggerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomationTriggerParameters", x => new { x.TriggerId, x.Identifier });
                    table.ForeignKey(
                        name: "FK_AutomationTriggerParameters_AutomationTriggers_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "AutomationTriggers",
                        principalColumn: "TriggerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutomationTriggerProviders",
                columns: table => new
                {
                    TriggerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomationTriggerProviders", x => x.TriggerId);
                    table.ForeignKey(
                        name: "FK_AutomationTriggerProviders_AutomationTriggers_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "AutomationTriggers",
                        principalColumn: "TriggerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutomationActions_AutomationId",
                table: "AutomationActions",
                column: "AutomationId");

            migrationBuilder.CreateIndex(
                name: "IX_Automations_OwnerId",
                table: "Automations",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AutomationTriggers_AutomationId",
                table: "AutomationTriggers",
                column: "AutomationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationLinkRequests_OwnerId",
                table: "IntegrationLinkRequests",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_OwnerId",
                table: "Integrations",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationTokens_IntegrationId",
                table: "IntegrationTokens",
                column: "IntegrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutomationActionParameters");

            migrationBuilder.DropTable(
                name: "AutomationActionProviders");

            migrationBuilder.DropTable(
                name: "AutomationTriggerParameters");

            migrationBuilder.DropTable(
                name: "AutomationTriggerProviders");

            migrationBuilder.DropTable(
                name: "IntegrationLinkRequests");

            migrationBuilder.DropTable(
                name: "IntegrationTokens");

            migrationBuilder.DropTable(
                name: "AutomationActions");

            migrationBuilder.DropTable(
                name: "AutomationTriggers");

            migrationBuilder.DropTable(
                name: "Integrations");

            migrationBuilder.DropTable(
                name: "Automations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
