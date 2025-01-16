using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAutomationProvidersConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AutomationTriggerProviders",
                table: "AutomationTriggerProviders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutomationTriggerProviders",
                table: "AutomationTriggerProviders",
                columns: new[] { "TriggerId", "ProviderId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AutomationTriggerProviders",
                table: "AutomationTriggerProviders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutomationTriggerProviders",
                table: "AutomationTriggerProviders",
                column: "TriggerId");
        }
    }
}
