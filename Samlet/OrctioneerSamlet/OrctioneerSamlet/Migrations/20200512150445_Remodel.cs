using Microsoft.EntityFrameworkCore.Migrations;

namespace VareDatabase.Migrations
{
    public partial class Remodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserIdBuyer",
                table: "Bids",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserIdBuyer",
                table: "Bids",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
