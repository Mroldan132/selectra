using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selectra.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurarRelacionVacaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiasVacacionesDisponibles",
                table: "Personales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "EstadoSolicitudVacaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoSolicitudVacaciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudVacaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personalId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoId = table.Column<int>(type: "int", nullable: false),
                    ComentariosEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComentariosAprobador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AprobadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudVacaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_EstadoSolicitudVacaciones_estadoId",
                        column: x => x.estadoId,
                        principalTable: "EstadoSolicitudVacaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_Personales_AprobadorId",
                        column: x => x.AprobadorId,
                        principalTable: "Personales",
                        principalColumn: "personalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_Personales_personalId",
                        column: x => x.personalId,
                        principalTable: "Personales",
                        principalColumn: "personalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_AprobadorId",
                table: "SolicitudVacaciones",
                column: "AprobadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_estadoId",
                table: "SolicitudVacaciones",
                column: "estadoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_personalId",
                table: "SolicitudVacaciones",
                column: "personalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudVacaciones");

            migrationBuilder.DropTable(
                name: "EstadoSolicitudVacaciones");

            migrationBuilder.DropColumn(
                name: "DiasVacacionesDisponibles",
                table: "Personales");
        }
    }
}
