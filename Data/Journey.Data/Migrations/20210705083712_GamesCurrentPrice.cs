using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class GamesCurrentPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                schema: "17114092",
                table: "Games",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                schema: "17114092",
                table: "Games");
        }
    }
}
