using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    ///
    /// 
    public partial class RenameProvidersFieldByDependencies : Migration
    {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AutomationActionProviders",
                newName: "AutomationActionDependencies");

            migrationBuilder.RenameTable(
                name: "AutomationTriggerProviders",
                newName: "AutomationTriggerDependencies");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "AutomationTriggerDependencies",
                newName: "DependencyId");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "AutomationActionDependencies",
                newName: "DependencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AutomationActionDependencies",
                newName: "AutomationActionProviders");

            migrationBuilder.RenameTable(
                name: "AutomationTriggerDependencies",
                newName: "AutomationTriggerProviders");

            migrationBuilder.RenameColumn(
                name: "DependencyId",
                table: "AutomationTriggerProviders",
                newName: "ProviderId");

            migrationBuilder.RenameColumn(
                name: "DependencyId",
                table: "AutomationActionProviders",
                newName: "ProviderId");
        }
    }
}
