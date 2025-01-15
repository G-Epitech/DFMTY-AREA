using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixIntegrationsTokenPKConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegrationTokens",
                table: "IntegrationTokens");

            migrationBuilder.DropIndex(
                name: "IX_IntegrationTokens_IntegrationId",
                table: "IntegrationTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegrationTokens",
                table: "IntegrationTokens",
                columns: new[] { "IntegrationId", "TokenType", "TokenUsage" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegrationTokens",
                table: "IntegrationTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegrationTokens",
                table: "IntegrationTokens",
                columns: new[] { "TokenValue", "TokenType", "TokenUsage" });

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationTokens_IntegrationId",
                table: "IntegrationTokens",
                column: "IntegrationId");
        }
    }
}
