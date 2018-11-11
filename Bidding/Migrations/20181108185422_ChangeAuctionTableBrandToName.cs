using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class ChangeAuctionTableBrandToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Auctions",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Auctions",
                newName: "Brand");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Auctions",
                nullable: true);
        }
    }
}
