using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class LogTableRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "Logs_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                schema: "17114092",
                table: "Logs_17114092",
                newName: "DateOfChange");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn_17114092",
                schema: "17114092",
                table: "GamesTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "GamesTags",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn_17114092",
                schema: "17114092",
                table: "GamesTags");

            migrationBuilder.DropColumn(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "GamesTags");

            migrationBuilder.RenameColumn(
                name: "DateOfChange",
                schema: "17114092",
                table: "Logs_17114092",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn_17114092",
                schema: "17114092",
                table: "Logs_17114092",
                type: "datetime2",
                nullable: true);
        }
    }
}
