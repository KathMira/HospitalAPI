using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class Imagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdImagemDocumento",
                table: "Pessoas",
                newName: "ImagemDocumentoId");

            migrationBuilder.AlterColumn<int>(
                name: "ConvenioId",
                table: "Pacientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    ImagemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeImagem = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoImagem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.ImagemId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_ImagemDocumentoId",
                table: "Pessoas",
                column: "ImagemDocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Imagens_ImagemDocumentoId",
                table: "Pessoas",
                column: "ImagemDocumentoId",
                principalTable: "Imagens",
                principalColumn: "ImagemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Imagens_ImagemDocumentoId",
                table: "Pessoas");

            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_ImagemDocumentoId",
                table: "Pessoas");

            migrationBuilder.RenameColumn(
                name: "ImagemDocumentoId",
                table: "Pessoas",
                newName: "IdImagemDocumento");

            migrationBuilder.AlterColumn<int>(
                name: "ConvenioId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
