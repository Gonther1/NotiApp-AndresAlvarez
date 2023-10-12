using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "modulosmaestros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreModulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulosmaestros", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "permisosgenericos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombrePermiso = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisosgenericos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "submodulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreSubModulo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submodulos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rolvsmaestro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdMaestro = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolvsmaestro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rolvsmaestro_modulosmaestros_IdMaestro",
                        column: x => x.IdMaestro,
                        principalTable: "modulosmaestros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolvsmaestro_rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "maestrosvssubmodulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdMaestro = table.Column<int>(type: "int", nullable: false),
                    IdSubmodulo = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maestrosvssubmodulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_maestrosvssubmodulos_modulosmaestros_IdMaestro",
                        column: x => x.IdMaestro,
                        principalTable: "modulosmaestros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_maestrosvssubmodulos_submodulos_IdSubmodulo",
                        column: x => x.IdSubmodulo,
                        principalTable: "submodulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "genericosvssubmodulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdGenericos = table.Column<int>(type: "int", nullable: false),
                    IdSubmodulos = table.Column<int>(type: "int", nullable: false),
                    IdRoles = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genericosvssubmodulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_genericosvssubmodulos_maestrosvssubmodulos_IdSubmodulos",
                        column: x => x.IdSubmodulos,
                        principalTable: "maestrosvssubmodulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_genericosvssubmodulos_permisosgenericos_IdGenericos",
                        column: x => x.IdGenericos,
                        principalTable: "permisosgenericos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_genericosvssubmodulos_rol_IdRoles",
                        column: x => x.IdRoles,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_genericosvssubmodulos_IdGenericos",
                table: "genericosvssubmodulos",
                column: "IdGenericos");

            migrationBuilder.CreateIndex(
                name: "IX_genericosvssubmodulos_IdRoles",
                table: "genericosvssubmodulos",
                column: "IdRoles");

            migrationBuilder.CreateIndex(
                name: "IX_genericosvssubmodulos_IdSubmodulos",
                table: "genericosvssubmodulos",
                column: "IdSubmodulos");

            migrationBuilder.CreateIndex(
                name: "IX_maestrosvssubmodulos_IdMaestro",
                table: "maestrosvssubmodulos",
                column: "IdMaestro");

            migrationBuilder.CreateIndex(
                name: "IX_maestrosvssubmodulos_IdSubmodulo",
                table: "maestrosvssubmodulos",
                column: "IdSubmodulo");

            migrationBuilder.CreateIndex(
                name: "IX_rolvsmaestro_IdMaestro",
                table: "rolvsmaestro",
                column: "IdMaestro");

            migrationBuilder.CreateIndex(
                name: "IX_rolvsmaestro_IdRol",
                table: "rolvsmaestro",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "genericosvssubmodulos");

            migrationBuilder.DropTable(
                name: "rolvsmaestro");

            migrationBuilder.DropTable(
                name: "maestrosvssubmodulos");

            migrationBuilder.DropTable(
                name: "permisosgenericos");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "modulosmaestros");

            migrationBuilder.DropTable(
                name: "submodulos");
        }
    }
}
