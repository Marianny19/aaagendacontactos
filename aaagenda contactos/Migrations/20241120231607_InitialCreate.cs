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
                name: "agendas",
                columns: table => new
                {
                    ID_agenda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_agenda = table.Column<string>(type: "text", nullable: false),
                    Descripcion_agenda = table.Column<string>(type: "text", nullable: false),
                    Fecha_agendada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Hora_agendada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ID_contacto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendas", x => x.ID_agenda);
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
                });

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
                name: "telefono",
                columns: table => new
                {
                    Id_telefono = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Número_de_teléfono = table.Column<string>(type: "text", nullable: false),
                    Tipo_teléfono = table.Column<string>(type: "text", nullable: false),
                    Id_contacto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefono", x => x.Id_telefono);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendas");

            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "red_sociall");

            migrationBuilder.DropTable(
                name: "telefono");

            migrationBuilder.DropTable(
                name: "tipos_contacto");

            migrationBuilder.DropTable(
                name: "tipos_red_social");
        }
    }
}
