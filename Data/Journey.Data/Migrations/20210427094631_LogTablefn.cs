using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class LogTablefn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfChange",
                schema: "17114092",
                table: "Logs_17114092",
                newName: "DateOfChange_17114092");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfChange_17114092",
                schema: "17114092",
                table: "Logs_17114092",
                newName: "DateOfChange");
        }
    }
}
