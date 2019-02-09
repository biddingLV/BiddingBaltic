using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "1", "Vieglais transports līdz 3,5t", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "2", "Traktortehnika", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "3", "Kravas auto", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "4", "Mototehnika", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "5", "Ūdens transports", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "6", "Cits transports", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "7", "Biroja tehnika", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "8", "Elektrotehnika", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "9", "Rūpniecības tehnika", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "10", "Instrumenti", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "11", "Cita manta", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "12", "Dzīvoklis", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "13", "Māja", true });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName", "TypeStatus" },
                values: new object[] { "14", "Zeme", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 14);
        }
    }
}
