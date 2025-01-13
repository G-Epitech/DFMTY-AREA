using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Zeus.Api.Domain.Authentication.AuthenticationMethodAggregate.Enums;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthenticationMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:AuthenticationMethodType", "google,password")
                .Annotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .Annotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .Annotation("Npgsql:Enum:IntegrationType", "discord,gmail")
                .OldAnnotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .OldAnnotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .OldAnnotation("Npgsql:Enum:IntegrationType", "discord,gmail");

            migrationBuilder.CreateTable(
                name: "AuthenticationMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<AuthenticationMethodType>(type: "\"AuthenticationMethodType\"", nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthenticationMethods_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationMethods_UserId",
                table: "AuthenticationMethods",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenticationMethods");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .Annotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .Annotation("Npgsql:Enum:IntegrationType", "discord,gmail")
                .OldAnnotation("Npgsql:Enum:AuthenticationMethodType", "google,password")
                .OldAnnotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .OldAnnotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .OldAnnotation("Npgsql:Enum:IntegrationType", "discord,gmail");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
