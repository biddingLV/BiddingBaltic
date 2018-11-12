using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Bidding.Migrations
{
    public partial class AddSeedDataForAuctionList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dateInString = "29/10/2018";
            DateTime startDate = DateTime.ParseExact(dateInString, "dd/MM/yyyy", null);
            DateTime expiryDate = startDate.AddDays(30);

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 1, "Balta un atra", "PORSCHE CAYENNE", 20000, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 2, "Melns un atrs", "BMW 530", 1800, "Masina", startDate, expiryDate });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "Brand", "Price", "Type", "StartDate", "EndDate" },
                values: new object[] { 3, "Balta un smaga", "Toyota land cruiser", 25000, "Masina", startDate, expiryDate });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
