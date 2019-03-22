using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulatePropertyStateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.InsertData(
                table: "PropertyStates",
                columns: new[] { "PropertyStateID", "PropertyStateName" },
                values: new object[] { "1", "Apdzīvots"});

            migrationBuilder.InsertData(
                table: "PropertyStates",
                columns: new[] { "PropertyStateId", "PropertyStateName"},
                values: new object[] { "2", "Neapdzīvots"});
                 
                 migrationBuilder.InsertData(
                table: "PropertyStates",
                columns: new[] { "PropertyStateId", "PropertyStateName"},
                values: new object[] { "3", "Nepieciešams remonts"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DeleteData(
                table: "PropertyStates",
                keyColumn: "PropertyStateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyStates",
                keyColumn: "PropertyStateId",
                keyValue: 2);
                 migrationBuilder.DeleteData(
                table: "PropertyStates",
                keyColumn: "PropertyStateId",
                keyValue: 3);
        }
    }
}
