using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateCategoryTypesTableWithRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 3, 1, 3 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 4, 1, 4 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 5, 1, 5 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 6, 1, 6 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 7, 2, 7 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 8, 2, 8 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 9, 2, 9 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 10, 2, 10 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 11, 2, 11 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 12, 3, 12 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 13, 3, 13 });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "CategoryId", "TypeId" },
                values: new object[] { 14, 3, 14 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
