using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParticipacionDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInquietudes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inquietudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquietudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquietudes_AspNetUsers_AutorId",
                        column: x => x.AutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasInquietud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InquietudId = table.Column<int>(type: "int", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    EsAutoridad = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasInquietud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasInquietud_AspNetUsers_AutorId",
                        column: x => x.AutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestasInquietud_Inquietudes_InquietudId",
                        column: x => x.InquietudId,
                        principalTable: "Inquietudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inquietudes_AutorId",
                table: "Inquietudes",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasInquietud_AutorId",
                table: "RespuestasInquietud",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasInquietud_InquietudId",
                table: "RespuestasInquietud",
                column: "InquietudId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespuestasInquietud");

            migrationBuilder.DropTable(
                name: "Inquietudes");
        }
    }
}
