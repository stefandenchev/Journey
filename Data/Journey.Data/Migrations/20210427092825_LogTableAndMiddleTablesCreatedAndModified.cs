using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class LogTableAndMiddleTablesCreatedAndModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn_17114092",
                schema: "17114092",
                table: "GamesLanguages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "GamesLanguages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn_17114092",
                schema: "17114092",
                table: "GamesGenres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "GamesGenres",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Logs_17114092",
                schema: "17114092",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs_17114092", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs_17114092",
                schema: "17114092");

            migrationBuilder.DropColumn(
                name: "CreatedOn_17114092",
                schema: "17114092",
                table: "GamesLanguages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "GamesLanguages");

            migrationBuilder.DropColumn(
                name: "CreatedOn_17114092",
                schema: "17114092",
                table: "GamesGenres");

            migrationBuilder.DropColumn(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "GamesGenres");
        }
    }
}
