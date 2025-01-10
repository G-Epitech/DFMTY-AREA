using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderIdToAuthMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProviderUserId",
                table: "AuthenticationMethods",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderUserId",
                table: "AuthenticationMethods");
        }
    }
}
