using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class CriandotabelaLaudo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laudo_Consultas_ConsultaId",
                table: "Laudo");

            migrationBuilder.DropForeignKey(
                name: "FK_Laudo_Exames_ExameId",
                table: "Laudo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laudo",
                table: "Laudo");

            migrationBuilder.RenameTable(
                name: "Laudo",
                newName: "Laudos");

            migrationBuilder.RenameIndex(
                name: "IX_Laudo_ExameId",
                table: "Laudos",
                newName: "IX_Laudos_ExameId");

            migrationBuilder.RenameIndex(
                name: "IX_Laudo_ConsultaId",
                table: "Laudos",
                newName: "IX_Laudos_ConsultaId");

            migrationBuilder.AlterColumn<string>(
                name: "CID",
                table: "Laudos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laudos",
                table: "Laudos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Laudos_Consultas_ConsultaId",
                table: "Laudos",
                column: "ConsultaId",
                principalTable: "Consultas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Laudos_Exames_ExameId",
                table: "Laudos",
                column: "ExameId",
                principalTable: "Exames",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laudos_Consultas_ConsultaId",
                table: "Laudos");

            migrationBuilder.DropForeignKey(
                name: "FK_Laudos_Exames_ExameId",
                table: "Laudos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laudos",
                table: "Laudos");

            migrationBuilder.RenameTable(
                name: "Laudos",
                newName: "Laudo");

            migrationBuilder.RenameIndex(
                name: "IX_Laudos_ExameId",
                table: "Laudo",
                newName: "IX_Laudo_ExameId");

            migrationBuilder.RenameIndex(
                name: "IX_Laudos_ConsultaId",
                table: "Laudo",
                newName: "IX_Laudo_ConsultaId");

            migrationBuilder.AlterColumn<string>(
                name: "CID",
                table: "Laudo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laudo",
                table: "Laudo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Laudo_Consultas_ConsultaId",
                table: "Laudo",
                column: "ConsultaId",
                principalTable: "Consultas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Laudo_Exames_ExameId",
                table: "Laudo",
                column: "ExameId",
                principalTable: "Exames",
                principalColumn: "Id");
        }
    }
}
