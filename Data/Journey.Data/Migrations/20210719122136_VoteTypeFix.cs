using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class VoteTypeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_ForumPosts_ForumPostId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_ForumPostId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "ForumPostId",
                table: "Votes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForumPostId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ForumPostId",
                table: "Votes",
                column: "ForumPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_ForumPosts_ForumPostId",
                table: "Votes",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
