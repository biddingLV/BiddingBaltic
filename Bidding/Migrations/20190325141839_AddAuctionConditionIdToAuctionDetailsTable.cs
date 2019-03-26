using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddAuctionConditionIdToAuctionDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.AddColumn<int>(
                name: "AuctionConditionId",
                table: "AuctionDetails",
                nullable: false,
                defaultValue: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DropColumn(
                name: "AuctionConditionId",
                table: "AuctionDetails");
        }
    }
}
