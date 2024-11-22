using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aaagenda_contactos.Migrations
{
    /// <inheritdoc />
    public partial class Actualizarrelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_contactos_Tipo_Contacto",
                table: "contactos",
                column: "Tipo_Contacto");

            migrationBuilder.CreateIndex(
                name: "IX_contactos_Tipo_red_social",
                table: "contactos",
                column: "Tipo_red_social");

            migrationBuilder.AddForeignKey(
                name: "FK_contactos_tipos_contacto_Tipo_Contacto",
                table: "contactos",
                column: "Tipo_Contacto",
                principalTable: "tipos_contacto",
                principalColumn: "ID_tipo_contacto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contactos_tipos_red_social_Tipo_red_social",
                table: "contactos",
                column: "Tipo_red_social",
                principalTable: "tipos_red_social",
                principalColumn: "Id_tipo_red_social",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contactos_tipos_contacto_Tipo_Contacto",
                table: "contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_contactos_tipos_red_social_Tipo_red_social",
                table: "contactos");

            migrationBuilder.DropIndex(
                name: "IX_contactos_Tipo_Contacto",
                table: "contactos");

            migrationBuilder.DropIndex(
                name: "IX_contactos_Tipo_red_social",
                table: "contactos");
        }
    }
}
