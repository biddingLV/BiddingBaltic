using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionConditionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuctionConditions",
                columns: new[] { "AuctionConditionId", "AuctionConditionName"},
                values: new object[] { "1", "Lietota"});
            migrationBuilder.InsertData(
                table: "AuctionConditions",
                columns: new[] { "AuctionConditionId", "AuctionConditionName"},
                values: new object[] { "2", "Jauna"});
            migrationBuilder.InsertData(
                table: "AuctionConditions",
                columns: new[] { "AuctionConditionId", "AuctionConditionName" },
                values: new object[] { "3", "Apdzīvots"});
            migrationBuilder.InsertData(
                table: "AuctionConditions",
                columns: new[] { "AuctionConditionId", "AuctionConditionName"},
                values: new object[] { "4", "Neapdzīvots"});     
            migrationBuilder.InsertData(
                table: "AuctionConditions",
                columns: new[] { "AuctionConditionId", "AuctionConditionName"},
                values: new object[] { "5", "Nepieciešams remonts"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuctionConditions",
                keyColumn: "AuctionConditionId",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "AuctionConditions",
                keyColumn: "AuctionConditionId",
                keyValue: 2);
            migrationBuilder.DeleteData(
                table: "AuctionConditions",
                keyColumn: "AuctionConditionId",
                keyValue: 3);
            migrationBuilder.DeleteData(
                table: "AuctionConditions",
                keyColumn: "AuctionConditionId",
                keyValue: 4);
            migrationBuilder.DeleteData(
                table: "AuctionConditions",
                keyColumn: "AuctionConditionId",
                keyValue: 5);
        }
    }
}
