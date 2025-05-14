using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selectra.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    areaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.areaId);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    cargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.cargoId);
                });

            migrationBuilder.CreateTable(
                name: "EstadosHistorialAprobaciones",
                columns: table => new
                {
                    estadoHistorialAprobacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoEstado = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    nombreEstado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosHistorialAprobaciones", x => x.estadoHistorialAprobacionId);
                });

            migrationBuilder.CreateTable(
                name: "EstadosOfertaLaborales",
                columns: table => new
                {
                    estadoOfertaLaboralId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoEstado = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    nombreEstado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    esPublica = table.Column<bool>(type: "bit", nullable: false),
                    esEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosOfertaLaborales", x => x.estadoOfertaLaboralId);
                });

            migrationBuilder.CreateTable(
                name: "EstadosPostulantes",
                columns: table => new
                {
                    estadoPostulanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoEstado = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    nombreEstado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    esEstadoContratacion = table.Column<bool>(type: "bit", nullable: false),
                    esEstadoRechazo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosPostulantes", x => x.estadoPostulanteId);
                });

            migrationBuilder.CreateTable(
                name: "EstadosRequerimientos",
                columns: table => new
                {
                    estadoRequerimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoEstado = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    nombreEstado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    esEstadoInicial = table.Column<bool>(type: "bit", nullable: false),
                    esEstadoFinal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosRequerimientos", x => x.estadoRequerimientoId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    rolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreRol = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nivel = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.rolId);
                });

            migrationBuilder.CreateTable(
                name: "TipoPreguntasFiltro",
                columns: table => new
                {
                    tipoPreguntaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPreguntasFiltro", x => x.tipoPreguntaId);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumentos",
                columns: table => new
                {
                    tipoDocumentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreTipoDocumento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumentos", x => x.tipoDocumentoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codUsuario = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    claveHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rolId = table.Column<int>(type: "int", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.usuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_rolId",
                        column: x => x.rolId,
                        principalTable: "Roles",
                        principalColumn: "rolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatosPersonales",
                columns: table => new
                {
                    datosPersonalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    apellidoPaterno = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    apellidoMaterno = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    nombres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    numeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    emailPersonal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ubigeoNacimiento = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ubigeoResidencia = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosPersonales", x => x.datosPersonalesId);
                    table.ForeignKey(
                        name: "FK_DatosPersonales_TiposDocumentos_tipoDocumentoId",
                        column: x => x.tipoDocumentoId,
                        principalTable: "TiposDocumentos",
                        principalColumn: "tipoDocumentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposRequerimientos",
                columns: table => new
                {
                    tipoRequerimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposRequerimientos", x => x.tipoRequerimientoId);
                    table.ForeignKey(
                        name: "FK_TiposRequerimientos_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Personales",
                columns: table => new
                {
                    personalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datosPersonalesId = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    jefeDirectoId = table.Column<int>(type: "int", nullable: true),
                    areaId = table.Column<int>(type: "int", nullable: false),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    emailCorporativo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaIngresoCompania = table.Column<DateTime>(type: "datetime2", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personales", x => x.personalId);
                    table.ForeignKey(
                        name: "FK_Personales_Areas_areaId",
                        column: x => x.areaId,
                        principalTable: "Areas",
                        principalColumn: "areaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personales_Cargos_cargoId",
                        column: x => x.cargoId,
                        principalTable: "Cargos",
                        principalColumn: "cargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personales_DatosPersonales_datosPersonalesId",
                        column: x => x.datosPersonalesId,
                        principalTable: "DatosPersonales",
                        principalColumn: "datosPersonalesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personales_Personales_jefeDirectoId",
                        column: x => x.jefeDirectoId,
                        principalTable: "Personales",
                        principalColumn: "personalId");
                    table.ForeignKey(
                        name: "FK_Personales_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesAprobaciones",
                columns: table => new
                {
                    ordenAprobacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoRequerimientoId = table.Column<int>(type: "int", nullable: false),
                    orden = table.Column<int>(type: "int", nullable: false),
                    rolAprobadorId = table.Column<int>(type: "int", nullable: false),
                    descripcionPaso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesAprobaciones", x => x.ordenAprobacionId);
                    table.ForeignKey(
                        name: "FK_OrdenesAprobaciones_Roles_rolAprobadorId",
                        column: x => x.rolAprobadorId,
                        principalTable: "Roles",
                        principalColumn: "rolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesAprobaciones_TiposRequerimientos_tipoRequerimientoId",
                        column: x => x.tipoRequerimientoId,
                        principalTable: "TiposRequerimientos",
                        principalColumn: "tipoRequerimientoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesAprobaciones_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "RequerimientosPersonales",
                columns: table => new
                {
                    requerimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoRequerimientoId = table.Column<int>(type: "int", nullable: false),
                    tituloRequerimiento = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    solicitanteId = table.Column<int>(type: "int", nullable: false),
                    areaId = table.Column<int>(type: "int", nullable: false),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sueldoPropuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    fechaDeseadaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    jefeDestinoId = table.Column<int>(type: "int", nullable: true),
                    estadoRequerimientoId = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true),
                    fechaFinProceso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequerimientosPersonales", x => x.requerimientoId);
                    table.ForeignKey(
                        name: "FK_RequerimientosPersonales_Areas_areaId",
                        column: x => x.areaId,
                        principalTable: "Areas",
                        principalColumn: "areaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequerimientosPersonales_Cargos_cargoId",
                        column: x => x.cargoId,
                        principalTable: "Cargos",
                        principalColumn: "cargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequerimientosPersonales_EstadosRequerimientos_estadoRequerimientoId",
                        column: x => x.estadoRequerimientoId,
                        principalTable: "EstadosRequerimientos",
                        principalColumn: "estadoRequerimientoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequerimientosPersonales_Personales_jefeDestinoId",
                        column: x => x.jefeDestinoId,
                        principalTable: "Personales",
                        principalColumn: "personalId");
                    table.ForeignKey(
                        name: "FK_RequerimientosPersonales_Personales_solicitanteId",
                        column: x => x.solicitanteId,
                        principalTable: "Personales",
                        principalColumn: "personalId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RequerimientosPersonales_TiposRequerimientos_tipoRequerimientoId",
                        column: x => x.tipoRequerimientoId,
                        principalTable: "TiposRequerimientos",
                        principalColumn: "tipoRequerimientoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequerimientosPersonales_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "HistorialAprobaciones",
                columns: table => new
                {
                    historialAprobacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    requerimientoId = table.Column<int>(type: "int", nullable: false),
                    ordenAprobacionId = table.Column<int>(type: "int", nullable: false),
                    aprobadorId = table.Column<int>(type: "int", nullable: false),
                    estadoHistorialAprobacionId = table.Column<int>(type: "int", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaDecision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAprobaciones", x => x.historialAprobacionId);
                    table.ForeignKey(
                        name: "FK_HistorialAprobaciones_EstadosHistorialAprobaciones_estadoHistorialAprobacionId",
                        column: x => x.estadoHistorialAprobacionId,
                        principalTable: "EstadosHistorialAprobaciones",
                        principalColumn: "estadoHistorialAprobacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialAprobaciones_OrdenesAprobaciones_ordenAprobacionId",
                        column: x => x.ordenAprobacionId,
                        principalTable: "OrdenesAprobaciones",
                        principalColumn: "ordenAprobacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialAprobaciones_Personales_aprobadorId",
                        column: x => x.aprobadorId,
                        principalTable: "Personales",
                        principalColumn: "personalId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HistorialAprobaciones_RequerimientosPersonales_requerimientoId",
                        column: x => x.requerimientoId,
                        principalTable: "RequerimientosPersonales",
                        principalColumn: "requerimientoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HistorialAprobaciones_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "OfertasLaborales",
                columns: table => new
                {
                    ofertaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    requerimientoId = table.Column<int>(type: "int", nullable: true),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    funciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    beneficios = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    competencias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sueldoOfrecido = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    areaId = table.Column<int>(type: "int", nullable: false),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    responsableId = table.Column<int>(type: "int", nullable: false),
                    direccionTrabajo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    referenciaUbicacion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    estadoOfertaLaboralId = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaEstimadaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertasLaborales", x => x.ofertaId);
                    table.ForeignKey(
                        name: "FK_OfertasLaborales_Areas_areaId",
                        column: x => x.areaId,
                        principalTable: "Areas",
                        principalColumn: "areaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertasLaborales_Cargos_cargoId",
                        column: x => x.cargoId,
                        principalTable: "Cargos",
                        principalColumn: "cargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertasLaborales_EstadosOfertaLaborales_estadoOfertaLaboralId",
                        column: x => x.estadoOfertaLaboralId,
                        principalTable: "EstadosOfertaLaborales",
                        principalColumn: "estadoOfertaLaboralId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertasLaborales_Personales_responsableId",
                        column: x => x.responsableId,
                        principalTable: "Personales",
                        principalColumn: "personalId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OfertasLaborales_RequerimientosPersonales_requerimientoId",
                        column: x => x.requerimientoId,
                        principalTable: "RequerimientosPersonales",
                        principalColumn: "requerimientoId");
                    table.ForeignKey(
                        name: "FK_OfertasLaborales_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Postulantes",
                columns: table => new
                {
                    postulanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ofertaId = table.Column<int>(type: "int", nullable: false),
                    nombreCompleto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tipoDocumento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    numeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    cvPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fechaPostulacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoPostulanteId = table.Column<int>(type: "int", nullable: false),
                    fuenteReclutamiento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postulantes", x => x.postulanteId);
                    table.ForeignKey(
                        name: "FK_Postulantes_EstadosPostulantes_estadoPostulanteId",
                        column: x => x.estadoPostulanteId,
                        principalTable: "EstadosPostulantes",
                        principalColumn: "estadoPostulanteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Postulantes_OfertasLaborales_ofertaId",
                        column: x => x.ofertaId,
                        principalTable: "OfertasLaborales",
                        principalColumn: "ofertaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Postulantes_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "PreguntasFiltros",
                columns: table => new
                {
                    preguntaFiltroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ofertaId = table.Column<int>(type: "int", nullable: false),
                    tipoPreguntaId = table.Column<int>(type: "int", nullable: false),
                    textoPregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasFiltros", x => x.preguntaFiltroId);
                    table.ForeignKey(
                        name: "FK_PreguntasFiltros_OfertasLaborales_ofertaId",
                        column: x => x.ofertaId,
                        principalTable: "OfertasLaborales",
                        principalColumn: "ofertaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreguntasFiltros_TipoPreguntasFiltro_tipoPreguntaId",
                        column: x => x.tipoPreguntaId,
                        principalTable: "TipoPreguntasFiltro",
                        principalColumn: "tipoPreguntaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreguntasFiltros_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "OpcionPreguntaFiltro",
                columns: table => new
                {
                    opcionPreguntaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    preguntaFiltroId = table.Column<int>(type: "int", nullable: false),
                    textoOpcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    orden = table.Column<int>(type: "int", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionPreguntaFiltro", x => x.opcionPreguntaId);
                    table.ForeignKey(
                        name: "FK_OpcionPreguntaFiltro_PreguntasFiltros_preguntaFiltroId",
                        column: x => x.preguntaFiltroId,
                        principalTable: "PreguntasFiltros",
                        principalColumn: "preguntaFiltroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpcionPreguntaFiltro_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateTable(
                name: "RespuestasPostulantes",
                columns: table => new
                {
                    respuestaPostulanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postulanteId = table.Column<int>(type: "int", nullable: false),
                    preguntaFiltroId = table.Column<int>(type: "int", nullable: false),
                    valorRespuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUltMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioUltModId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasPostulantes", x => x.respuestaPostulanteId);
                    table.ForeignKey(
                        name: "FK_RespuestasPostulantes_Postulantes_postulanteId",
                        column: x => x.postulanteId,
                        principalTable: "Postulantes",
                        principalColumn: "postulanteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespuestasPostulantes_PreguntasFiltros_preguntaFiltroId",
                        column: x => x.preguntaFiltroId,
                        principalTable: "PreguntasFiltros",
                        principalColumn: "preguntaFiltroId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RespuestasPostulantes_Usuarios_usuarioUltModId",
                        column: x => x.usuarioUltModId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatosPersonales_tipoDocumentoId",
                table: "DatosPersonales",
                column: "tipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAprobaciones_aprobadorId",
                table: "HistorialAprobaciones",
                column: "aprobadorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAprobaciones_estadoHistorialAprobacionId",
                table: "HistorialAprobaciones",
                column: "estadoHistorialAprobacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAprobaciones_ordenAprobacionId",
                table: "HistorialAprobaciones",
                column: "ordenAprobacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAprobaciones_requerimientoId",
                table: "HistorialAprobaciones",
                column: "requerimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAprobaciones_usuarioUltModId",
                table: "HistorialAprobaciones",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_areaId",
                table: "OfertasLaborales",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_cargoId",
                table: "OfertasLaborales",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_estadoOfertaLaboralId",
                table: "OfertasLaborales",
                column: "estadoOfertaLaboralId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_requerimientoId",
                table: "OfertasLaborales",
                column: "requerimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_responsableId",
                table: "OfertasLaborales",
                column: "responsableId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasLaborales_usuarioUltModId",
                table: "OfertasLaborales",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcionPreguntaFiltro_preguntaFiltroId",
                table: "OpcionPreguntaFiltro",
                column: "preguntaFiltroId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcionPreguntaFiltro_usuarioUltModId",
                table: "OpcionPreguntaFiltro",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesAprobaciones_rolAprobadorId",
                table: "OrdenesAprobaciones",
                column: "rolAprobadorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesAprobaciones_tipoRequerimientoId",
                table: "OrdenesAprobaciones",
                column: "tipoRequerimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesAprobaciones_usuarioUltModId",
                table: "OrdenesAprobaciones",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_Personales_areaId",
                table: "Personales",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_Personales_cargoId",
                table: "Personales",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personales_datosPersonalesId",
                table: "Personales",
                column: "datosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Personales_jefeDirectoId",
                table: "Personales",
                column: "jefeDirectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personales_usuarioId",
                table: "Personales",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulantes_estadoPostulanteId",
                table: "Postulantes",
                column: "estadoPostulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulantes_ofertaId",
                table: "Postulantes",
                column: "ofertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulantes_usuarioUltModId",
                table: "Postulantes",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasFiltros_ofertaId",
                table: "PreguntasFiltros",
                column: "ofertaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasFiltros_tipoPreguntaId",
                table: "PreguntasFiltros",
                column: "tipoPreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasFiltros_usuarioUltModId",
                table: "PreguntasFiltros",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerimientosPersonales_areaId",
                table: "RequerimientosPersonales",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerimientosPersonales_cargoId",
                table: "RequerimientosPersonales",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerimientosPersonales_estadoRequerimientoId",
                table: "RequerimientosPersonales",
                column: "estadoRequerimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerimientosPersonales_jefeDestinoId",
                table: "RequerimientosPersonales",
                column: "jefeDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerimientosPersonales_solicitanteId",
                table: "RequerimientosPersonales",
                column: "solicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerimientosPersonales_tipoRequerimientoId",
                table: "RequerimientosPersonales",
                column: "tipoRequerimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerimientosPersonales_usuarioUltModId",
                table: "RequerimientosPersonales",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasPostulantes_postulanteId",
                table: "RespuestasPostulantes",
                column: "postulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasPostulantes_preguntaFiltroId",
                table: "RespuestasPostulantes",
                column: "preguntaFiltroId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasPostulantes_usuarioUltModId",
                table: "RespuestasPostulantes",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposRequerimientos_usuarioUltModId",
                table: "TiposRequerimientos",
                column: "usuarioUltModId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_rolId",
                table: "Usuarios",
                column: "rolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialAprobaciones");

            migrationBuilder.DropTable(
                name: "OpcionPreguntaFiltro");

            migrationBuilder.DropTable(
                name: "RespuestasPostulantes");

            migrationBuilder.DropTable(
                name: "EstadosHistorialAprobaciones");

            migrationBuilder.DropTable(
                name: "OrdenesAprobaciones");

            migrationBuilder.DropTable(
                name: "Postulantes");

            migrationBuilder.DropTable(
                name: "PreguntasFiltros");

            migrationBuilder.DropTable(
                name: "EstadosPostulantes");

            migrationBuilder.DropTable(
                name: "OfertasLaborales");

            migrationBuilder.DropTable(
                name: "TipoPreguntasFiltro");

            migrationBuilder.DropTable(
                name: "EstadosOfertaLaborales");

            migrationBuilder.DropTable(
                name: "RequerimientosPersonales");

            migrationBuilder.DropTable(
                name: "EstadosRequerimientos");

            migrationBuilder.DropTable(
                name: "Personales");

            migrationBuilder.DropTable(
                name: "TiposRequerimientos");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "DatosPersonales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TiposDocumentos");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
