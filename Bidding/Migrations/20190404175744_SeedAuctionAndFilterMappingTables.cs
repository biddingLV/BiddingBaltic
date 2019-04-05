using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class SeedAuctionAndFilterMappingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 3 },
                    { 3, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "AuctionId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 3 },
                    { 3, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "CategoryTypeId", "CategoryId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 3);
        }
    }
}
