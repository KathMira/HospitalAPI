using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class Atualizandotabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Exames",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Exames");
        }
    }
}
