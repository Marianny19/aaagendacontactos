using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace aaagenda_contactos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "red_sociall",
                columns: table => new
                {
                    Id_red_social = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_de_usuario = table.Column<string>(type: "text", nullable: false),
                    ID_contacto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_red_sociall", x => x.Id_red_social);
                });

            migrationBuilder.CreateTable(
                name: "tipos_contacto",
                columns: table => new
                {
                    ID_tipo_contacto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_tipo_contacto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_contacto", x => x.ID_tipo_contacto);
                });

            migrationBuilder.CreateTable(
                name: "tipos_red_social",
                columns: table => new
                {
                    Id_tipo_red_social = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_red_social = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_red_social", x => x.Id_tipo_red_social);
                });

            migrationBuilder.CreateTable(
                name: "contactos",
                columns: table => new
                {
                    ID_contacto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Tipo_Contacto = table.Column<int>(type: "integer", nullable: false),
                    Tipo_red_social = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactos", x => x.ID_contacto);
                    table.ForeignKey(
                        name: "FK_contactos_tipos_contacto_Tipo_Contacto",
                        column: x => x.Tipo_Contacto,
                        principalTable: "tipos_contacto",
                        principalColumn: "ID_tipo_contacto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contactos_tipos_red_social_Tipo_red_social",
                        column: x => x.Tipo_red_social,
                        principalTable: "tipos_red_social",
                        principalColumn: "Id_tipo_red_social",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agendas",
                columns: table => new
                {
                    ID_agenda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_agenda = table.Column<string>(type: "text", nullable: false),
                    Descripcion_agenda = table.Column<string>(type: "text", nullable: false),
                    Fecha_agendada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ID_contacto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendas", x => x.ID_agenda);
                    table.ForeignKey(
                        name: "FK_agendas_contactos_ID_contacto",
                        column: x => x.ID_contacto,
                        principalTable: "contactos",
                        principalColumn: "ID_contacto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telefono",
                columns: table => new
                {
                    Id_telefono = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Número_de_teléfono = table.Column<string>(type: "text", nullable: false),
                    Tipo_teléfono = table.Column<string>(type: "text", nullable: false),
                    Id_contacto = table.Column<int>(type: "integer", nullable: false),
                    contactoID_contacto = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefono", x => x.Id_telefono);
                    table.ForeignKey(
                        name: "FK_telefono_contactos_contactoID_contacto",
                        column: x => x.contactoID_contacto,
                        principalTable: "contactos",
                        principalColumn: "ID_contacto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_agendas_ID_contacto",
                table: "agendas",
                column: "ID_contacto");

            migrationBuilder.CreateIndex(
                name: "IX_contactos_Tipo_Contacto",
                table: "contactos",
                column: "Tipo_Contacto");

            migrationBuilder.CreateIndex(
                name: "IX_contactos_Tipo_red_social",
                table: "contactos",
                column: "Tipo_red_social");

            migrationBuilder.CreateIndex(
                name: "IX_telefono_contactoID_contacto",
                table: "telefono",
                column: "contactoID_contacto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendas");

            migrationBuilder.DropTable(
                name: "red_sociall");

            migrationBuilder.DropTable(
                name: "telefono");

            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "tipos_contacto");

            migrationBuilder.DropTable(
                name: "tipos_red_social");
        }
    }
}
