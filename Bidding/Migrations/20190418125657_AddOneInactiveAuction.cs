using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddOneInactiveAuction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "ApplyDate", "AuctionStatusId", "CreatedAt", "CreatedBy", "Deleted", "EndDate", "LastUpdatedAt", "LastUpdatedBy", "Name", "StartDate", "StartingPrice" },
                values: new object[] { 4, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Audi A4", new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 350 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 4, 4, 1 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "AuctionId", "TypeId" },
                values: new object[] { 4, 4, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "AuctionId",
                keyValue: 4);
        }
    }
}
