using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionStatusesTableAndAddStatusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionStatuses",
                columns: new[] { "AuctionStatusId", "AuctionStatusName", "Status" },
                values: new object[] { "1", "Aktīva", true });

            migrationBuilder.InsertData(
                table: "AuctionStatuses",
                columns: new[] { "AuctionStatusId", "AuctionStatusName", "Status" },
                values: new object[] { "2", "Pārtraukta", true });

            migrationBuilder.InsertData(
                table: "AuctionStatuses",
                columns: new[] { "AuctionStatusId", "AuctionStatusName", "Status" },
                values: new object[] { "3", "Beigusies", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionStatuses",
                keyColumn: "AuctionStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                 table: "AuctionStatuses",
                 keyColumn: "AuctionStatusId",
                 keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuctionStatuses",
                keyColumn: "AuctionStatusId",
                keyValue: 3);
        }
    }
}
