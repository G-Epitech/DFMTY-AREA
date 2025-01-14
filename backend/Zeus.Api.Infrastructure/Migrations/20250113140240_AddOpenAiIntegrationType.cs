using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeus.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOpenAiIntegrationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:AuthenticationMethodType", "google,password")
                .Annotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .Annotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .Annotation("Npgsql:Enum:IntegrationType", "discord,gmail,notion,open_ai")
                .OldAnnotation("Npgsql:Enum:AuthenticationMethodType", "google,password")
                .OldAnnotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .OldAnnotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .OldAnnotation("Npgsql:Enum:IntegrationType", "discord,gmail,notion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:AuthenticationMethodType", "google,password")
                .Annotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .Annotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .Annotation("Npgsql:Enum:IntegrationType", "discord,gmail,notion")
                .OldAnnotation("Npgsql:Enum:AuthenticationMethodType", "google,password")
                .OldAnnotation("Npgsql:Enum:AutomationActionParameterType", "raw,var")
                .OldAnnotation("Npgsql:Enum:IntegrationTokenUsage", "access,refresh")
                .OldAnnotation("Npgsql:Enum:IntegrationType", "discord,gmail,notion,open_ai");
        }
    }
}
