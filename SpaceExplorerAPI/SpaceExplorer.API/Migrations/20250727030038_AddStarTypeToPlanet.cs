using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceExplorer.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStarTypeToPlanet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StarType",
                table: "Planets",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarType",
                table: "Planets");
        }
    }
}
