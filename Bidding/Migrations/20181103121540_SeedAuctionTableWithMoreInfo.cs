using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Bidding.Migrations
{
    public partial class SeedAuctionTableWithMoreInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dateInString = "03/11/2018";
            DateTime startDate = DateTime.Parse(dateInString);
            DateTime expiryDate = startDate.AddDays(30);

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 4, "Balta un atra", "Skoda Citigo", 5000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 5, "Melns un atrs", "Skoda Fabia", 6000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 6, "Balta un smaga", "Skoda Forman", 7000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 7, "Balta un smaga", "Skoda Kodiaq", 8000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 8, "Balta un smaga", "Skoda Octavia", 9000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 9, "Balta un smaga", "Skoda Praktik", 10000, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 10, "Balta un smaga", "Skoda Rapid", 6500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 11, "Balta un smaga", "Skoda Roomster", 7500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 12, "Balta un smaga", "Skoda Superb", 8500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 13, "Balta un smaga", "Skoda Yeti", 9500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 14, "Balta un smaga", "Toyota 4-runner", 10000, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 15, "Balta un smaga", "Toyota auris", 3400, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 16, "Balta un smaga", "Toyota avensis", 500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 17, "Balta un smaga", "Toyota aygo", 700, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 18, "Balta un smaga", "Toyota camry", 879, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 19, "Balta un smaga", "Toyota carina e", 544, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 20, "Balta un smaga", "Toyota celicca", 1000, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 21, "Balta un smaga", "Toyota corolla", 4500, "Masina", startDate, expiryDate });


            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 22, "Balta un smaga", "Toyota dyna", 3333, "Masina", startDate, expiryDate });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 22);
        }
    }
}
