using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoLaudo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Laudo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataLaudo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomeLaudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    DescricaoLaudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExameId = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    ConsultaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laudo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laudo_Consultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consultas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Laudo_Exames_ExameId",
                        column: x => x.ExameId,
                        principalTable: "Exames",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laudo_ConsultaId",
                table: "Laudo",
                column: "ConsultaId");

            migrationBuilder.CreateIndex(
                name: "IX_Laudo_ExameId",
                table: "Laudo",
                column: "ExameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laudo");
        }
    }
}
