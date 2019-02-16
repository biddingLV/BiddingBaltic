using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 3, 1, 3 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 4, 6, 4 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 5, 1, 5 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 6, 2, 6 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 7, 3, 7 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 8, 12, 8 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 9, 5, 9 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 10, 7, 10 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 11, 8, 11 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 12, 8, 12 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 13, 8, 13 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 14, 13, 14 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 15, 11, 15 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 16, 6, 16 });

            migrationBuilder.InsertData(
                table: "AuctionTypes",
                columns: new[] { "AuctionTypeId", "TypeId", "AuctionId" },
                values: new object[] { 17, 10, 17 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                 table: "AuctionTypes",
                 keyColumn: "AuctionTypeId",
                 keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                 table: "AuctionTypes",
                 keyColumn: "AuctionTypeId",
                 keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                 table: "AuctionTypes",
                 keyColumn: "AuctionTypeId",
                 keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                 table: "AuctionTypes",
                 keyColumn: "AuctionTypeId",
                 keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                 table: "AuctionTypes",
                 keyColumn: "AuctionTypeId",
                 keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AuctionTypes",
                keyColumn: "AuctionTypeId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                 table: "AuctionTypes",
                 keyColumn: "AuctionTypeId",
                 keyValue: 17);
        }
    }
}
