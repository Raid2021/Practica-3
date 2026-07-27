using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVotacion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial_Votantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Votantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YaVoto = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votantes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votantes_Cedula",
                table: "Votantes",
                column: "Cedula",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votantes");
        }
    }
}
