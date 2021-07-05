using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class GamesOnSalePercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalePercentage",
                schema: "17114092",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePercentage",
                schema: "17114092",
                table: "Games");
        }
    }
}
