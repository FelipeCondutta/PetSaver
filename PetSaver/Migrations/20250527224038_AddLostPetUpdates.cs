using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSaver.Migrations
{
    /// <inheritdoc />
    public partial class AddLostPetUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChipCode",
                table: "LostPets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "LostPets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "LostPets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "LostPets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LostPets_Users_UserId",
                table: "LostPets");

            migrationBuilder.DropIndex(
                name: "IX_LostPets_UserId",
                table: "LostPets");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "LostPets");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "LostPets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "LostPets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LostPets");

            migrationBuilder.AlterColumn<string>(
                name: "ChipCode",
                table: "LostPets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
