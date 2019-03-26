using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddAuctionFormatIdToAuctionDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuctionFormatId",
                table: "AuctionDetails",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DropColumn(
                name: "AuctionFormatId",
                table: "AuctionDetails");
        }
    }
}
