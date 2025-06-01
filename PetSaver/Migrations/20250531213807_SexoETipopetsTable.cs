using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSaver.Migrations
{
    /// <inheritdoc />
    public partial class SexoETipopetsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Pets");
        }
    }
}
