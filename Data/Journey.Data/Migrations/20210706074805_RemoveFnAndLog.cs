using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class RemoveFnAndLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs_17114092",
                schema: "17114092");

            migrationBuilder.RenameTable(
                name: "Wishlists",
                schema: "17114092",
                newName: "Wishlists");

            migrationBuilder.RenameTable(
                name: "Votes",
                schema: "17114092",
                newName: "Votes");

            migrationBuilder.RenameTable(
                name: "UserCartItems",
                schema: "17114092",
                newName: "UserCartItems");

            migrationBuilder.RenameTable(
                name: "Tags",
                schema: "17114092",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Publishers",
                schema: "17114092",
                newName: "Publishers");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "17114092",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                schema: "17114092",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Languages",
                schema: "17114092",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Images",
                schema: "17114092",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Genres",
                schema: "17114092",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "GamesTags",
                schema: "17114092",
                newName: "GamesTags");

            migrationBuilder.RenameTable(
                name: "GamesLanguages",
                schema: "17114092",
                newName: "GamesLanguages");

            migrationBuilder.RenameTable(
                name: "Games",
                schema: "17114092",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "CreditCards",
                schema: "17114092",
                newName: "CreditCards");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "17114092",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "17114092",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "17114092",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "17114092",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "17114092",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "17114092",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "17114092",
                newName: "AspNetRoleClaims");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "17114092");

            migrationBuilder.RenameTable(
                name: "Wishlists",
                newName: "Wishlists",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "Votes",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "UserCartItems",
                newName: "UserCartItems",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tags",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "Publishers",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Orders",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItems",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Languages",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Images",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genres",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "GamesTags",
                newName: "GamesTags",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "GamesLanguages",
                newName: "GamesLanguages",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Games",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "CreditCards",
                newName: "CreditCards",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "17114092");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "17114092");

            migrationBuilder.CreateTable(
                name: "Logs_17114092",
                schema: "17114092",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfChange_17114092 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs_17114092", x => x.Id);
                });
        }
    }
}
