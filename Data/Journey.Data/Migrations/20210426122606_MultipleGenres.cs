using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class MultipleGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Genres_GenreId",
                schema: "17114092",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GenreId",
                schema: "17114092",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GenreId",
                schema: "17114092",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GamesGenres",
                schema: "17114092",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesGenres_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "17114092",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamesGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "17114092",
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamesGenres_GameId",
                schema: "17114092",
                table: "GamesGenres",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesGenres_GenreId",
                schema: "17114092",
                table: "GamesGenres",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesGenres",
                schema: "17114092");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                schema: "17114092",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenreId",
                schema: "17114092",
                table: "Games",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Genres_GenreId",
                schema: "17114092",
                table: "Games",
                column: "GenreId",
                principalSchema: "17114092",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
