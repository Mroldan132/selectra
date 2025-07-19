using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selectra.Migrations
{
    /// <inheritdoc />
    public partial class SincronizarModeloActual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 👌 Ya no se renombra la tabla ni se agrega PK, porque ya existen

            // 🌱 Insertar datos semilla
            migrationBuilder.InsertData(
                table: "TipoPreguntasFiltro",
                columns: new[] { "tipoPreguntaId", "nombre" },
                values: new object[,]
                {
                    { 1, "Conocimientos Generales" },
                    { 2, "Aptitudes Técnicas" },
                    { 3, "Habilidades Blandas" }
                });

            // 🔗 Agregar nuevas claves foráneas
            migrationBuilder.AddForeignKey(
                name: "FK_OfertasLaborales_TipoPreguntasFiltro_tipoPreguntaFiltroId",
                table: "OfertasLaborales",
                column: "tipoPreguntaFiltroId",
                principalTable: "TipoPreguntasFiltro",
                principalColumn: "tipoPreguntaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntasFiltros_TipoPreguntasFiltro_tipoPreguntaId",
                table: "PreguntasFiltros",
                column: "tipoPreguntaId",
                principalTable: "TipoPreguntasFiltro",
                principalColumn: "tipoPreguntaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 🔻 Eliminar claves foráneas nuevas
            migrationBuilder.DropForeignKey(
                name: "FK_OfertasLaborales_TipoPreguntasFiltro_tipoPreguntaFiltroId",
                table: "OfertasLaborales");

            migrationBuilder.DropForeignKey(
                name: "FK_PreguntasFiltros_TipoPreguntasFiltro_tipoPreguntaId",
                table: "PreguntasFiltros");

            // 🧹 Eliminar datos semilla
            migrationBuilder.DeleteData(
                table: "TipoPreguntasFiltro",
                keyColumn: "tipoPreguntaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoPreguntasFiltro",
                keyColumn: "tipoPreguntaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoPreguntasFiltro",
                keyColumn: "tipoPreguntaId",
                keyValue: 3);

            // 🔄 Renombrar tabla de regreso
            migrationBuilder.RenameTable(
                name: "TipoPreguntasFiltro",
                newName: "TipoPreguntasFiltros");

            // ✔️ Reagregar PK original (ya que ahora sí se renombra)
            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPreguntasFiltros",
                table: "TipoPreguntasFiltros",
                column: "tipoPreguntaId");

            // 🔗 Restaurar claves foráneas antiguas
            migrationBuilder.AddForeignKey(
                name: "FK_OfertasLaborales_TipoPreguntasFiltros_tipoPreguntaFiltroId",
                table: "OfertasLaborales",
                column: "tipoPreguntaFiltroId",
                principalTable: "TipoPreguntasFiltros",
                principalColumn: "tipoPreguntaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntasFiltros_TipoPreguntasFiltros_tipoPreguntaId",
                table: "PreguntasFiltros",
                column: "tipoPreguntaId",
                principalTable: "TipoPreguntasFiltros",
                principalColumn: "tipoPreguntaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}