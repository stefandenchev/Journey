using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class ChangeDboToFn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "17114092");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "Settings",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Settings",
                schema: "17114092",
                newName: "Settings");

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
    }
}
