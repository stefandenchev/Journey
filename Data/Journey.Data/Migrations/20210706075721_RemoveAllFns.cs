using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class RemoveAllFns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Wishlists",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Wishlists",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Votes",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Votes",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "UserCartItems",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "UserCartItems",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Tags",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Tags",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Publishers",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Publishers",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Orders",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Orders",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "OrderItems",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "OrderItems",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Languages",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Languages",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Images",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Images",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Genres",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Genres",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "GamesTags",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "GamesTags",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "GamesLanguages",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "GamesLanguages",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "Games",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "Games",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "CreditCards",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "CreditCards",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "AspNetUsers",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "AspNetUsers",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn_17114092",
                table: "AspNetRoles",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn_17114092",
                table: "AspNetRoles",
                newName: "CreatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Wishlists",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Wishlists",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Votes",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Votes",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "UserCartItems",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "UserCartItems",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Tags",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Tags",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Publishers",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Publishers",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Orders",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Orders",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "OrderItems",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "OrderItems",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Languages",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Languages",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Images",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Images",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Genres",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Genres",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "GamesTags",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "GamesTags",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "GamesLanguages",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "GamesLanguages",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Games",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Games",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "CreditCards",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "CreditCards",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "AspNetUsers",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "AspNetUsers",
                newName: "CreatedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "AspNetRoles",
                newName: "ModifiedOn_17114092");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "AspNetRoles",
                newName: "CreatedOn_17114092");
        }
    }
}
