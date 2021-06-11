using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class Trailers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos",
                schema: "17114092");

            migrationBuilder.DropColumn(
                name: "VideoId",
                schema: "17114092",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                schema: "17114092",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                schema: "17114092",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                schema: "17114092",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Videos",
                schema: "17114092",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Games_Id",
                        column: x => x.Id,
                        principalSchema: "17114092",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_IsDeleted",
                schema: "17114092",
                table: "Videos",
                column: "IsDeleted");
        }
    }
}
