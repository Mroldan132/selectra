using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selectra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpcionPreguntaFiltro_PreguntasFiltros_preguntaFiltroId",
                table: "OpcionPreguntaFiltro");

            migrationBuilder.DropForeignKey(
                name: "FK_OpcionPreguntaFiltro_Usuarios_usuarioUltModId",
                table: "OpcionPreguntaFiltro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpcionPreguntaFiltro",
                table: "OpcionPreguntaFiltro");

            migrationBuilder.RenameTable(
                name: "OpcionPreguntaFiltro",
                newName: "OpcionesPreguntasFiltros");

            migrationBuilder.RenameIndex(
                name: "IX_OpcionPreguntaFiltro_usuarioUltModId",
                table: "OpcionesPreguntasFiltros",
                newName: "IX_OpcionesPreguntasFiltros_usuarioUltModId");

            migrationBuilder.RenameIndex(
                name: "IX_OpcionPreguntaFiltro_preguntaFiltroId",
                table: "OpcionesPreguntasFiltros",
                newName: "IX_OpcionesPreguntasFiltros_preguntaFiltroId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpcionesPreguntasFiltros",
                table: "OpcionesPreguntasFiltros",
                column: "opcionPreguntaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpcionesPreguntasFiltros_PreguntasFiltros_preguntaFiltroId",
                table: "OpcionesPreguntasFiltros",
                column: "preguntaFiltroId",
                principalTable: "PreguntasFiltros",
                principalColumn: "preguntaFiltroId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpcionesPreguntasFiltros_Usuarios_usuarioUltModId",
                table: "OpcionesPreguntasFiltros",
                column: "usuarioUltModId",
                principalTable: "Usuarios",
                principalColumn: "usuarioId");
        }
    }
}
