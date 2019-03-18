using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulatePropertyStateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.InsertData(
                table: "PropertyState",
                columns: new[] { "PropertyStateID", "PropertyStateName" },
                values: new object[] { "1", "Apdzīvots"});

            migrationBuilder.InsertData(
                table: "PropertyState",
                columns: new[] { "PropertyStateId", "PropertyStateName"},
                values: new object[] { "2", "Neapdzīvots"});
                 
                 migrationBuilder.InsertData(
                table: "PropertyState",
                columns: new[] { "PropertyStateId", "PropertyStateName"},
                values: new object[] { "3", "Nepieciešams remonts"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DeleteData(
                table: "PropertyState",
                keyColumn: "PropertyStateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyState",
                keyColumn: "PropertyStateId",
                keyValue: 2);
                 migrationBuilder.DeleteData(
                table: "PropertyState",
                keyColumn: "PropertyStateId",
                keyValue: 3);
        }
    }
}
