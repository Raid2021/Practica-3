using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVotacion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIndiceUnicoVotoVotante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votos_VotanteId",
                table: "Votos");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_VotanteId",
                table: "Votos",
                column: "VotanteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votos_VotanteId",
                table: "Votos");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_VotanteId",
                table: "Votos",
                column: "VotanteId");
        }
    }
}
