using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dateInString = "01/12/2018";
            DateTime startDate = DateTime.Parse(dateInString);
            DateTime expiryDate = startDate.AddDays(365);

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 1, "Tesla Model 3", 15000, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 2, "Ferrari F12 TRS", 2800, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 3, "Porsche Boxster", 6660, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 4, "Virgin Airbus A330-200", 965000, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 5, "LADA VAZ-2101 (1970)", 150, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 6, "MB Trac 65/70", 1200, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 7, "Volvo Truck, Volvo VHD", 5000, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 8, "Penthouse tipa dzīvoklis Vecrīgas sirdī", 158640, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 9, "Beneteau Swift Trawler 50", 3800, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 10, "EGG™ EASY CHAIR, LEATHER - Fritz Hansen", 1560, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 11, "13-inch MacBook Pro", 480, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 12, "Fridge - Samsung Family Hub 2.0", 110, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 13, "Bosch MultiTalent Food Processor", 332, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 14, "Īpašums Bulduros", 5550, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 15, "Vīna skapis", 159, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 16, "Velosipēds", 25, startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" },
                values: new object[] { 17, "Gaisa aerators dīķim, 500 ltr gaisa stundā.", 69, startDate, expiryDate });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 17);
        }
    }
}
