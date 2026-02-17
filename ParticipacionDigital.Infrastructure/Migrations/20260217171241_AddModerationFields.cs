using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParticipacionDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModerationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "RespuestasInquietudes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Inquietudes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Admoniciones",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "RespuestasInquietudes");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Inquietudes");

            migrationBuilder.DropColumn(
                name: "Admoniciones",
                table: "AspNetUsers");
        }
    }
}
