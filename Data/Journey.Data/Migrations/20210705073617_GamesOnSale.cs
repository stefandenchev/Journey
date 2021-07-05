using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class GamesOnSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                schema: "17114092",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnSale",
                schema: "17114092",
                table: "Games");
        }
    }
}
