using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class WishlistDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wishlists",
                schema: "17114092",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "17114092",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wishlists_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "17114092",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_GameId",
                schema: "17114092",
                table: "Wishlists",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_IsDeleted",
                schema: "17114092",
                table: "Wishlists",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                schema: "17114092",
                table: "Wishlists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wishlists",
                schema: "17114092");
        }
    }
}
