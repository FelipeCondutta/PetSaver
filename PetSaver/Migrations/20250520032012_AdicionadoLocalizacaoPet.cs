using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSaver.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoLocalizacaoPet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "FoundedPets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "FoundedPets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "FoundedPets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "FoundedPets");
        }
    }
}
