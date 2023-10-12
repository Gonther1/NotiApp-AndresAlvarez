using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auditoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreUsuario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescAccion = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditoria", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "estadonotificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreEstado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadonotificacion", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "formatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreFormato = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formatos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "hilorespuestanotificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTipo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hilorespuestanotificacion", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "radicados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_radicados", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tiponotificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTipo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiponotificaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tiporequerimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiporequerimiento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "blockchain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HashGenerado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdNotificacion = table.Column<int>(type: "int", nullable: false),
                    IdHiloRespuesta = table.Column<int>(type: "int", nullable: false),
                    IdAuditoria = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blockchain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_blockchain_auditoria_IdAuditoria",
                        column: x => x.IdAuditoria,
                        principalTable: "auditoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blockchain_hilorespuestanotificacion_IdHiloRespuesta",
                        column: x => x.IdHiloRespuesta,
                        principalTable: "hilorespuestanotificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blockchain_tiponotificaciones_IdNotificacion",
                        column: x => x.IdNotificacion,
                        principalTable: "tiponotificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "modulonotificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AsuntoNotificacion = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextoNotificacion = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdTipoNotificacion = table.Column<int>(type: "int", nullable: false),
                    IdRadicado = table.Column<int>(type: "int", nullable: false),
                    IdEstadoNotificacion = table.Column<int>(type: "int", nullable: false),
                    IdHiloRespuesta = table.Column<int>(type: "int", nullable: false),
                    IdFormato = table.Column<int>(type: "int", nullable: false),
                    IdRequerimiento = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulonotificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_modulonotificaciones_estadonotificacion_IdEstadoNotificacion",
                        column: x => x.IdEstadoNotificacion,
                        principalTable: "estadonotificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_modulonotificaciones_formatos_IdFormato",
                        column: x => x.IdFormato,
                        principalTable: "formatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_modulonotificaciones_hilorespuestanotificacion_IdHiloRespues~",
                        column: x => x.IdHiloRespuesta,
                        principalTable: "hilorespuestanotificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_modulonotificaciones_radicados_IdRadicado",
                        column: x => x.IdRadicado,
                        principalTable: "radicados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_modulonotificaciones_tiponotificaciones_IdTipoNotificacion",
                        column: x => x.IdTipoNotificacion,
                        principalTable: "tiponotificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_modulonotificaciones_tiporequerimiento_IdRequerimiento",
                        column: x => x.IdRequerimiento,
                        principalTable: "tiporequerimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_blockchain_IdAuditoria",
                table: "blockchain",
                column: "IdAuditoria");

            migrationBuilder.CreateIndex(
                name: "IX_blockchain_IdHiloRespuesta",
                table: "blockchain",
                column: "IdHiloRespuesta");

            migrationBuilder.CreateIndex(
                name: "IX_blockchain_IdNotificacion",
                table: "blockchain",
                column: "IdNotificacion");

            migrationBuilder.CreateIndex(
                name: "IX_modulonotificaciones_IdEstadoNotificacion",
                table: "modulonotificaciones",
                column: "IdEstadoNotificacion");

            migrationBuilder.CreateIndex(
                name: "IX_modulonotificaciones_IdFormato",
                table: "modulonotificaciones",
                column: "IdFormato");

            migrationBuilder.CreateIndex(
                name: "IX_modulonotificaciones_IdHiloRespuesta",
                table: "modulonotificaciones",
                column: "IdHiloRespuesta");

            migrationBuilder.CreateIndex(
                name: "IX_modulonotificaciones_IdRadicado",
                table: "modulonotificaciones",
                column: "IdRadicado");

            migrationBuilder.CreateIndex(
                name: "IX_modulonotificaciones_IdRequerimiento",
                table: "modulonotificaciones",
                column: "IdRequerimiento");

            migrationBuilder.CreateIndex(
                name: "IX_modulonotificaciones_IdTipoNotificacion",
                table: "modulonotificaciones",
                column: "IdTipoNotificacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blockchain");

            migrationBuilder.DropTable(
                name: "modulonotificaciones");

            migrationBuilder.DropTable(
                name: "auditoria");

            migrationBuilder.DropTable(
                name: "estadonotificacion");

            migrationBuilder.DropTable(
                name: "formatos");

            migrationBuilder.DropTable(
                name: "hilorespuestanotificacion");

            migrationBuilder.DropTable(
                name: "radicados");

            migrationBuilder.DropTable(
                name: "tiponotificaciones");

            migrationBuilder.DropTable(
                name: "tiporequerimiento");
        }
    }
}
