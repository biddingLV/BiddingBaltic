using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bidding.Migrations
{
    public partial class AddAuctionStatusIdToAuctionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuctionStatusId",
                table: "Auctions",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AuctionStatuses_AuctionStatusId",
                table: "Auctions",
                column: "AuctionStatusId",
                principalTable: "AuctionStatuses",
                principalColumn: "AuctionStatusId",
                onDelete: ReferentialAction.Cascade);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AuctionDetails_AuctionStatuses_AuctionStatusId",
            //     table: "AuctionDetails",
            //     column: "AuctionStatusId",
            //     principalTable: "AuctionStatuses",
            //     principalColumn: "AuctionStatusId",
            //     onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AuctionStatuses_AuctionStatusId",
                table: "Auctions");
             migrationBuilder.DropColumn(
                name: "AuctionStatusId",
                table: "Auctions");
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AuctionStatuses_AuctionStatusId",
                table: "AuctionDetails");
        }
    }
}
