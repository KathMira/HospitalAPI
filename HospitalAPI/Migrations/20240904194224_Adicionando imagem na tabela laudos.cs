using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class Adicionandoimagemnatabelalaudos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laudos_Imagens_ImagemDocumentoImagemId",
                table: "Laudos");

            migrationBuilder.RenameColumn(
                name: "ImagemDocumentoImagemId",
                table: "Laudos",
                newName: "ImagemDocumentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Laudos_ImagemDocumentoImagemId",
                table: "Laudos",
                newName: "IX_Laudos_ImagemDocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laudos_Imagens_ImagemDocumentoId",
                table: "Laudos",
                column: "ImagemDocumentoId",
                principalTable: "Imagens",
                principalColumn: "ImagemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laudos_Imagens_ImagemDocumentoId",
                table: "Laudos");

            migrationBuilder.RenameColumn(
                name: "ImagemDocumentoId",
                table: "Laudos",
                newName: "ImagemDocumentoImagemId");

            migrationBuilder.RenameIndex(
                name: "IX_Laudos_ImagemDocumentoId",
                table: "Laudos",
                newName: "IX_Laudos_ImagemDocumentoImagemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laudos_Imagens_ImagemDocumentoImagemId",
                table: "Laudos",
                column: "ImagemDocumentoImagemId",
                principalTable: "Imagens",
                principalColumn: "ImagemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
