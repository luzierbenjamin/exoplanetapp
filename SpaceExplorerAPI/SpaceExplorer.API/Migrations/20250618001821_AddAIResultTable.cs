using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceExplorer.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAIResultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AIResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanetID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlanetType = table.Column<string>(type: "TEXT", nullable: false),
                    Atmoshpere = table.Column<string>(type: "TEXT", nullable: false),
                    WaterLikelihood = table.Column<double>(type: "REAL", nullable: true),
                    BioScore = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIResults_Planets_PlanetID",
                        column: x => x.PlanetID,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIResults_PlanetID",
                table: "AIResults",
                column: "PlanetID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIResults");
        }
    }
}
