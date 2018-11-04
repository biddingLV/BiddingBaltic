using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Bidding.Migrations
{
    public partial class SeedAuctionListWithExtraInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dateInString = "04/11/2018";
            DateTime startDate = DateTime.Parse(dateInString);
            DateTime expiryDate = startDate.AddDays(30);

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 23, "Balta un atra", "Skoda Citigo", 5000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 24, "Melns un atrs", "Skoda Fabia", 6000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 25, "Balta un smaga", "Skoda Forman", 7000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 26, "Balta un smaga", "Skoda Kodiaq", 8000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 27, "Balta un smaga", "Skoda Octavia", 9000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 28, "Balta un smaga", "Skoda Praktik", 10000, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 29, "Balta un smaga", "Skoda Rapid", 6500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 30, "Balta un smaga", "Skoda Roomster", 7500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 31, "Balta un smaga", "Skoda Superb", 8500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 32, "Balta un smaga", "Skoda Yeti", 9500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 33, "Balta un smaga", "Toyota 4-runner", 10000, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 34, "Balta un smaga", "Toyota auris", 3400, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 35, "Balta un smaga", "Toyota avensis", 500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 36, "Balta un smaga", "Toyota aygo", 700, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 37, "Balta un smaga", "Toyota camry", 879, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 38, "Balta un smaga", "Toyota carina e", 544, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 39, "Balta un smaga", "Toyota celicca", 1000, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 40, "Balta un smaga", "Toyota corolla", 4500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 41, "Balta un smaga", "Toyota dyna", 3333, "Masina", startDate, expiryDate });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 41);
        }
    }
}
