using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace aaagenda_contactos.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agendas",
                columns: table => new
                {
                    ID_agenda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_agenda = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion_agenda = table.Column<string>(type: "text", nullable: false),
                    Fecha_agendada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Hora_agendada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendas", x => x.ID_agenda);
                });

            migrationBuilder.CreateTable(
                name: "TiposContacto",
                columns: table => new
                {
                    ID_tipo_contacto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_tipo_contacto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposContacto", x => x.ID_tipo_contacto);
                });

            migrationBuilder.CreateTable(
                name: "TiposRedSocial",
                columns: table => new
                {
                    Id_tipo_red_social = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_red_social = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposRedSocial", x => x.Id_tipo_red_social);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    ID_contacto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ID_Tipo_Contacto = table.Column<int>(type: "integer", nullable: false),
                    ID_Tipo_red_social = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.ID_contacto);
                    table.ForeignKey(
                        name: "FK_Contactos_TiposContacto_ID_Tipo_Contacto",
                        column: x => x.ID_Tipo_Contacto,
                        principalTable: "TiposContacto",
                        principalColumn: "ID_tipo_contacto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contactos_TiposRedSocial_ID_Tipo_red_social",
                        column: x => x.ID_Tipo_red_social,
                        principalTable: "TiposRedSocial",
                        principalColumn: "Id_tipo_red_social",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedesSociales",
                columns: table => new
                {
                    Id_red_social = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_de_usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ID_contacto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedesSociales", x => x.Id_red_social);
                    table.ForeignKey(
                        name: "FK_RedesSociales_Contactos_ID_contacto",
                        column: x => x.ID_contacto,
                        principalTable: "Contactos",
                        principalColumn: "ID_contacto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    Id_telefono = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Número_de_teléfono = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Tipo_teléfono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Id_contacto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.Id_telefono);
                    table.ForeignKey(
                        name: "FK_Telefonos_Contactos_Id_contacto",
                        column: x => x.Id_contacto,
                        principalTable: "Contactos",
                        principalColumn: "ID_contacto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ID_Tipo_Contacto",
                table: "Contactos",
                column: "ID_Tipo_Contacto");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ID_Tipo_red_social",
                table: "Contactos",
                column: "ID_Tipo_red_social");

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociales_ID_contacto",
                table: "RedesSociales",
                column: "ID_contacto");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_Id_contacto",
                table: "Telefonos",
                column: "Id_contacto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendas");

            migrationBuilder.DropTable(
                name: "RedesSociales");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "TiposContacto");

            migrationBuilder.DropTable(
                name: "TiposRedSocial");
        }
    }
}
