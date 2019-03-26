using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionFormatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.InsertData(
                table: "AuctionFormats",
                columns: new[] { "AuctionFormatId", "AuctionFormatName" },
                values: new object[] { "1", "Cenu aptauja"});

                migrationBuilder.InsertData(
                table: "AuctionFormats",
                columns: new[] { "AuctionFormatId", "AuctionFormatName"},
                values: new object[] { "2", "Izsole elektroniski"});

                migrationBuilder.InsertData(
                table: "AuctionFormats",
                columns: new[] { "AuctionFormatId", "AuctionFormatName"},
                values: new object[] { "3", "Izsole klātienē"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DeleteData(
                table: "AuctionFormatss",
                keyColumn: "AuctionFormatId",
                keyValue: 1);

                migrationBuilder.DeleteData(
                table: "AuctionFormats",
                keyColumn: "AuctionFormatId",
                keyValue: 2);

                 migrationBuilder.DeleteData(
                table: "AuctionFormats",
                keyColumn: "AuctionFormatId",
                keyValue: 3);
        }
    }
}
