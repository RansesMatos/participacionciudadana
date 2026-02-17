using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParticipacionDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAlcaldiasAndCedula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlcaldiaId",
                table: "Encuestas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlcaldiaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Alcaldias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alcaldias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_AlcaldiaId",
                table: "Encuestas",
                column: "AlcaldiaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AlcaldiaId",
                table: "AspNetUsers",
                column: "AlcaldiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Alcaldias_AlcaldiaId",
                table: "AspNetUsers",
                column: "AlcaldiaId",
                principalTable: "Alcaldias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Encuestas_Alcaldias_AlcaldiaId",
                table: "Encuestas",
                column: "AlcaldiaId",
                principalTable: "Alcaldias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Alcaldias_AlcaldiaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Encuestas_Alcaldias_AlcaldiaId",
                table: "Encuestas");

            migrationBuilder.DropTable(
                name: "Alcaldias");

            migrationBuilder.DropIndex(
                name: "IX_Encuestas_AlcaldiaId",
                table: "Encuestas");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AlcaldiaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AlcaldiaId",
                table: "Encuestas");

            migrationBuilder.DropColumn(
                name: "AlcaldiaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "AspNetUsers");
        }
    }
}
