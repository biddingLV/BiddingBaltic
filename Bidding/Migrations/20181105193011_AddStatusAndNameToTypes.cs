using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddStatusAndNameToTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Types",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Types",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Types");
        }
    }
}
