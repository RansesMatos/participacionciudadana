using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParticipacionDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAmonestaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amonestaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Razon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RazonLevantamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaLevantamiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminLevantamientoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amonestaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amonestaciones_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Amonestaciones_AspNetUsers_AdminLevantamientoId",
                        column: x => x.AdminLevantamientoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Amonestaciones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amonestaciones_AdminId",
                table: "Amonestaciones",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Amonestaciones_AdminLevantamientoId",
                table: "Amonestaciones",
                column: "AdminLevantamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Amonestaciones_UsuarioId",
                table: "Amonestaciones",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amonestaciones");
        }
    }
}
