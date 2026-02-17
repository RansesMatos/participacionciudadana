using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParticipacionDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActividades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRealizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagenPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlcaldiaId = table.Column<int>(type: "int", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actividades_Alcaldias_AlcaldiaId",
                        column: x => x.AlcaldiaId,
                        principalTable: "Alcaldias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_AlcaldiaId",
                table: "Actividades",
                column: "AlcaldiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividades");
        }
    }
}
