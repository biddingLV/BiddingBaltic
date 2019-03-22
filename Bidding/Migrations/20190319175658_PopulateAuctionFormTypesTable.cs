using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateAuctionFormTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.InsertData(
                table: "AuctionFormTypes",
                columns: new[] { "AuctionFormTypeId", "AuctionFormTypeName" },
                values: new object[] { "1", "Cenu aptauja"});

                migrationBuilder.InsertData(
                table: "AuctionFormTypes",
                columns: new[] { "AuctionFormTypeId", "AuctionFormTypeName"},
                values: new object[] { "2", "Izsole elektroniski"});

                migrationBuilder.InsertData(
                table: "AuctionFormTypes",
                columns: new[] { "AuctionFormTypeId", "AuctionFormTypeName"},
                values: new object[] { "3", "Izsole klātienē"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DeleteData(
                table: "AuctionFormTypes",
                keyColumn: "AuctionFormTypeId",
                keyValue: 1);

                migrationBuilder.DeleteData(
                table: "AuctionFormTypes",
                keyColumn: "AuctionFormTypeId",
                keyValue: 2);

                 migrationBuilder.DeleteData(
                table: "AuctionFormTypes",
                keyColumn: "AuctionFormTypeId",
                keyValue: 3);
        }
    }
}
