using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 3, 3, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 4, 4, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 5, 5, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 6, 6, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 7, 7, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 8, 8, 3 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 9, 9, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 10, 10, 2 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 11, 11, 2 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 12, 12, 2 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 13, 13, 2 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 14, 14, 3 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 15, 15, 2 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 16, 16, 1 });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[] { 17, 17, 2 });
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
                 table: "AuctionCategories",
                 keyColumn: "AuctionCategoryId",
                 keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                 table: "AuctionCategories",
                 keyColumn: "AuctionCategoryId",
                 keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                 table: "AuctionCategories",
                 keyColumn: "AuctionCategoryId",
                 keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                 table: "AuctionCategories",
                 keyColumn: "AuctionCategoryId",
                 keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                 table: "AuctionCategories",
                 keyColumn: "AuctionCategoryId",
                 keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                 table: "AuctionCategories",
                 keyColumn: "AuctionCategoryId",
                 keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                 table: "AuctionCategories",
                 keyColumn: "AuctionCategoryId",
                 keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AuctionCategories",
                keyColumn: "AuctionCategoryId",
                keyValue: 17);
        }
    }
}
