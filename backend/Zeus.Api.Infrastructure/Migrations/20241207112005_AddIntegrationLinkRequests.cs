using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Zeus.Api.Domain.Integrations.Common.Enums;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIntegrationLinkRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntegrationLinkRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<IntegrationType>(type: "\"IntegrationType\"", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationLinkRequests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationLinkRequests");
        }
    }
}
