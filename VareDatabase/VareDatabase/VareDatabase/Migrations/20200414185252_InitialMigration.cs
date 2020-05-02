using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VareDatabase.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "BId",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID_forLastBid = table.Column<int>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    userID_forSeller = table.Column<int>(nullable: false),
                    ItemRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BId", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BId_Item_ItemRef",
                        column: x => x.ItemRef,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageOfItem = table.Column<string>(nullable: true),
                    descriptionOfItem = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    ItemRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Description_Item_ItemRef",
                        column: x => x.ItemRef,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    experation = table.Column<string>(nullable: true),
                    timeOfCreation = table.Column<DateTime>(nullable: false),
                    ItemRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Time_Item_ItemRef",
                        column: x => x.ItemRef,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BId_ItemRef",
                table: "BId",
                column: "ItemRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Description_ItemRef",
                table: "Description",
                column: "ItemRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Time_ItemRef",
                table: "Time",
                column: "ItemRef",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BId");

            migrationBuilder.DropTable(
                name: "Description");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
