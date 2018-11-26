using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class SeedAuctionCreatorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionCreators",
                columns: new[] { "Id", "FirstName", "LastName", "Status" },
                values: new object[] { 1, "Kristaps", "Magic", true });

            migrationBuilder.InsertData(
                table: "AuctionCreators",
                columns: new[] { "Id", "FirstName", "LastName", "Status" },
                values: new object[] { 2, "Janis", "Magic", true });

            migrationBuilder.InsertData(
                table: "AuctionCreators",
                columns: new[] { "Id", "FirstName", "LastName", "Status" },
                values: new object[] { 3, "Zane", "Magic", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionCreators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuctionCreators",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuctionCreators",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
