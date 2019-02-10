using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionDetails",
                columns: new[] { "AuctionDetailsId", "AuctionId", "AuctionStatusId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "AuctionDetails",
                columns: new[] { "AuctionDetailsId", "AuctionId", "AuctionStatusId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.InsertData(
                table: "AuctionDetails",
                columns: new[] { "AuctionDetailsId", "AuctionId", "AuctionStatusId" },
                values: new object[] { 3, 3, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionDetails",
                keyColumn: "AuctionDetailsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                 table: "AuctionDetails",
                 keyColumn: "AuctionDetailsId",
                 keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuctionDetails",
                keyColumn: "AuctionDetailsId",
                keyValue: 3);
        }
    }
}
