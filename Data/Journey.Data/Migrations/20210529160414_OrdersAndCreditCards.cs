using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Journey.Data.Migrations
{
    public partial class OrdersAndCreditCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                schema: "17114092",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "17114092",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "17114092",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreditCardId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn_17114092 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "17114092",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalSchema: "17114092",
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_IsDeleted",
                schema: "17114092",
                table: "CreditCards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserId",
                schema: "17114092",
                table: "CreditCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreditCardId",
                schema: "17114092",
                table: "Orders",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "17114092",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "17114092");

            migrationBuilder.DropTable(
                name: "CreditCards",
                schema: "17114092");
        }
    }
}
