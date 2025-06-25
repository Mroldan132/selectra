using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selectra.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimaAcreditacionVacaciones",
                table: "Personales",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaUltimaAcreditacionVacaciones",
                table: "Personales");
        }
    }
}
