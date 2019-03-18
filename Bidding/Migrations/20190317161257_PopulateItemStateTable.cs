using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateItemStateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
              migrationBuilder.InsertData(
                table: "ItemState",
                columns: new[] { "ItemStateID", "ItemStateName" },
                values: new object[] { "1", "Lietota"});

            migrationBuilder.InsertData(
                table: "ItemState",
                columns: new[] { "ItemStateId", "ItemStateName"},
                values: new object[] { "2", "Jauna"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DeleteData(
                table: "ItemState",
                keyColumn: "ItemStateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemState",
                keyColumn: "ItemStateId",
                keyValue: 2);
        }
    }
}
