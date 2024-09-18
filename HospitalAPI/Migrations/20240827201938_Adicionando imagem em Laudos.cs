using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoimagememLaudos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImagemDocumentoId",
                table: "Laudos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Laudos_ImagemDocumentoId",
                table: "Laudos",
                column: "ImagemDocumentoId");

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

            migrationBuilder.DropIndex(
                name: "IX_Laudos_ImagemDocumentoId",
                table: "Laudos");

            migrationBuilder.DropColumn(
                name: "ImagemDocumentoId",
                table: "Laudos");
        }
    }
}
