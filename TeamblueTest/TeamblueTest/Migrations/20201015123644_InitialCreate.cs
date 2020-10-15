using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamblueTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UniqueWords",
                columns: table => new
                {
                    Word = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueWords", x => x.Word);
                });

            migrationBuilder.CreateTable(
                name: "WatchListWords",
                columns: table => new
                {
                    Word = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchListWords", x => x.Word);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniqueWords");

            migrationBuilder.DropTable(
                name: "WatchListWords");
        }
    }
}
