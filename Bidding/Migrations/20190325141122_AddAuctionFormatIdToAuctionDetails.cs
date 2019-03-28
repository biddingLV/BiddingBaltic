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
            
            migrationBuilder.AddForeignKey(
                name: "FK_AuctionDetails_AuctionFormats_AuctionFormatId",
                table: "AuctionDetails",
                column: "AuctionFormatId",
                principalTable: "AuctionFormats",
                principalColumn: "AuctionFormatId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionFormatId",
                table: "AuctionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionDetails_AuctionConditions_AuctionConditionId",
                table: "AuctionDetails");
        }
    }
}
