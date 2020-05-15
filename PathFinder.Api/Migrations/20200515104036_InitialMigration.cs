using Microsoft.EntityFrameworkCore.Migrations;

namespace PathFinder.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PathResult",
                columns: table => new
                {
                    InputArrayString = table.Column<string>(nullable: false),
                    IsTraversable = table.Column<bool>(nullable: false),
                    ResultArrayString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathResult", x => x.InputArrayString);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PathResult");
        }
    }
}
