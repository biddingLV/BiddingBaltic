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

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionDetails_AuctionConditions_AuctionConditionId",
                table: "AuctionDetails",
                column: "AuctionConditionId",
                principalTable: "AuctionConditions",
                principalColumn: "AuctionConditionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionDetails_AuctionConditions_AuctionConditionId",
                table: "AuctionDetails");

                migrationBuilder.DropColumn(
                name: "AuctionConditionId",
                table: "AuctionDetails");
        }
    }
}
