using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selectra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataPreguntas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_PreguntasFiltros_TipoPreguntasFiltros_tipoPreguntaId",
    table: "PreguntasFiltros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPreguntasFiltros",
                table: "TipoPreguntasFiltros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPreguntasFiltros",
                table: "TipoPreguntasFiltros",
                column: "tipoPreguntaId");
                
            migrationBuilder.AddForeignKey(
                name: "FK_PreguntasFiltros_TipoPreguntasFiltros_tipoPreguntaId",
                table: "PreguntasFiltros",
                column: "tipoPreguntaId",
                principalTable: "TipoPreguntasFiltros",
                principalColumn: "tipoPreguntaId",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPreguntasFiltros",
                table: "TipoPreguntasFiltros");

            migrationBuilder.RenameTable(
                name: "TipoPreguntasFiltros",
                newName: "TipoPreguntasFiltro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPreguntasFiltro",
                table: "TipoPreguntasFiltro",
                column: "tipoPreguntaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntasFiltros_TipoPreguntasFiltro_tipoPreguntaId",
                table: "PreguntasFiltros",
                column: "tipoPreguntaId",
                principalTable: "TipoPreguntasFiltro",
                principalColumn: "tipoPreguntaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
