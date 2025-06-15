using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceExplorer.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DiscoveryMethod = table.Column<string>(type: "TEXT", nullable: false),
                    DiscoveryYear = table.Column<int>(type: "INTEGER", nullable: true),
                    Radius = table.Column<double>(type: "REAL", nullable: true),
                    Mass = table.Column<double>(type: "REAL", nullable: true),
                    OrbitalPeriod = table.Column<double>(type: "REAL", nullable: true),
                    DistanceFromEarth = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}
