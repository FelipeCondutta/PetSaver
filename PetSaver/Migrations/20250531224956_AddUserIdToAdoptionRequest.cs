using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSaver.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToAdoptionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Users_UserId",
                table: "AdoptionRequests");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequests_UserId",
                table: "AdoptionRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdoptionRequests");
        }
    }
}
