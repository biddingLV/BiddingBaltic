using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionDetailsWithInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionDetails",
                columns: new[] { "Id", "AuctionId", "AuctionType", "Evaluation", "VehicleIdentificationNumber", "VehicleRegistrationNumber", "Year" },
                values: new object[] { 1, 1, "Klātienē", 980, null, "HH4424", "2005" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionDetails",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
