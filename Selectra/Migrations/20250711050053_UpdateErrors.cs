using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selectra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertasLaborales_TipoPreguntasFiltros_tipoPreguntaFiltroId",
                table: "OfertasLaborales");

            migrationBuilder.DropForeignKey(
                name: "FK_PreguntasFiltros_TipoPreguntasFiltros_tipoPreguntaId",
                table: "PreguntasFiltros");

            migrationBuilder.DropIndex(
                name: "IX_PreguntasFiltros_tipoPreguntaId",
                table: "PreguntasFiltros");

            migrationBuilder.DropIndex(
                name: "IX_OfertasLaborales_tipoPreguntaFiltroId",
                table: "OfertasLaborales");

            migrationBuilder.AddColumn<int>(
                name: "TipoPreguntasFiltrostipoPreguntaId",
                table: "PreguntasFiltros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoPreguntasFiltrostipoPreguntaId",
                table: "OfertasLaborales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasFiltros_TipoPreguntasFiltrostipoPreguntaId",
                table: "PreguntasFiltros",
                column: "TipoPreguntasFiltrostipoPreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_TipoPreguntasFiltrostipoPreguntaId",
                table: "OfertasLaborales",
                column: "TipoPreguntasFiltrostipoPreguntaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasLaborales_TipoPreguntasFiltros_TipoPreguntasFiltrostipoPreguntaId",
                table: "OfertasLaborales",
                column: "TipoPreguntasFiltrostipoPreguntaId",
                principalTable: "TipoPreguntasFiltros",
                principalColumn: "tipoPreguntaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntasFiltros_TipoPreguntasFiltros_TipoPreguntasFiltrostipoPreguntaId",
                table: "PreguntasFiltros",
                column: "TipoPreguntasFiltrostipoPreguntaId",
                principalTable: "TipoPreguntasFiltros",
                principalColumn: "tipoPreguntaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
