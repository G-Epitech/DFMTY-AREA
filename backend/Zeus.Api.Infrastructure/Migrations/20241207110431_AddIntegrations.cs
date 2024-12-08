using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Zeus.Api.Domain.Integrations.Common.Enums;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIntegrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:IntegrationType", "discord,gmail");

            migrationBuilder.CreateTable(
                name: "Integrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<IntegrationType>(type: "\"IntegrationType\"", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationTokens",
                columns: table => new
                {
                    TokenValue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TokenType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TokenUsage = table.Column<int>(type: "integer", nullable: false),
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
                name: "IntegrationTokens");

            migrationBuilder.DropTable(
                name: "Integrations");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:IntegrationType", "discord,gmail");
        }
    }
}
