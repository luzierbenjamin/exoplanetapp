using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceExplorer.API.Migrations
{
    /// <inheritdoc />
    public partial class InitFixedAIResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Atmoshpere",
                table: "AIResults",
                newName: "Atmosphere");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Atmosphere",
                table: "AIResults",
                newName: "Atmoshpere");
        }
    }
}
