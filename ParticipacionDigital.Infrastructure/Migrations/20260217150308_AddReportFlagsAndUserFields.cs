using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParticipacionDigital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReportFlagsAndUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasInquietud_AspNetUsers_AutorId",
                table: "RespuestasInquietud");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasInquietud_Inquietudes_InquietudId",
                table: "RespuestasInquietud");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RespuestasInquietud",
                table: "RespuestasInquietud");

            migrationBuilder.RenameTable(
                name: "RespuestasInquietud",
                newName: "RespuestasInquietudes");

            migrationBuilder.RenameIndex(
                name: "IX_RespuestasInquietud_InquietudId",
                table: "RespuestasInquietudes",
                newName: "IX_RespuestasInquietudes_InquietudId");

            migrationBuilder.RenameIndex(
                name: "IX_RespuestasInquietud_AutorId",
                table: "RespuestasInquietudes",
                newName: "IX_RespuestasInquietudes_AutorId");

            migrationBuilder.AddColumn<bool>(
                name: "Reportado",
                table: "Inquietudes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Reportado",
                table: "RespuestasInquietudes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RespuestasInquietudes",
                table: "RespuestasInquietudes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasInquietudes_AspNetUsers_AutorId",
                table: "RespuestasInquietudes",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasInquietudes_Inquietudes_InquietudId",
                table: "RespuestasInquietudes",
                column: "InquietudId",
                principalTable: "Inquietudes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasInquietudes_AspNetUsers_AutorId",
                table: "RespuestasInquietudes");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasInquietudes_Inquietudes_InquietudId",
                table: "RespuestasInquietudes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RespuestasInquietudes",
                table: "RespuestasInquietudes");

            migrationBuilder.DropColumn(
                name: "Reportado",
                table: "Inquietudes");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Reportado",
                table: "RespuestasInquietudes");

            migrationBuilder.RenameTable(
                name: "RespuestasInquietudes",
                newName: "RespuestasInquietud");

            migrationBuilder.RenameIndex(
                name: "IX_RespuestasInquietudes_InquietudId",
                table: "RespuestasInquietud",
                newName: "IX_RespuestasInquietud_InquietudId");

            migrationBuilder.RenameIndex(
                name: "IX_RespuestasInquietudes_AutorId",
                table: "RespuestasInquietud",
                newName: "IX_RespuestasInquietud_AutorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RespuestasInquietud",
                table: "RespuestasInquietud",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasInquietud_AspNetUsers_AutorId",
                table: "RespuestasInquietud",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasInquietud_Inquietudes_InquietudId",
                table: "RespuestasInquietud",
                column: "InquietudId",
                principalTable: "Inquietudes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
