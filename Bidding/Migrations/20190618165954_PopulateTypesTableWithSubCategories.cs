using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateTypesTableWithSubCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ItemAuctionDetails",
                keyColumn: "ItemAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 18, 16, 59, 53, 915, DateTimeKind.Utc).AddTicks(4914));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 9,
                column: "Name",
                value: "Sadzīves tehnika");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 10,
                column: "Name",
                value: "Instrumenti");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 11,
                column: "Name",
                value: "Iekārtas");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 12,
                column: "Name",
                value: "Ražošanas materiāli");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 13,
                columns: new[] { "AuctionCategoryId", "Name" },
                values: new object[] { 2, "Veikala produkcija" });

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 14,
                columns: new[] { "AuctionCategoryId", "Name" },
                values: new object[] { 2, "Uzņēmums" });

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 15,
                columns: new[] { "AuctionCategoryId", "Name" },
                values: new object[] { 2, "Domeins" });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "AuctionCategoryId", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 23, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Telpa" },
                    { 16, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Preču zīme" },
                    { 17, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Sadzīves mēbeles" },
                    { 18, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Biroja mēbeles" },
                    { 19, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cita manta" },
                    { 20, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dzīvoklis" },
                    { 21, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Māja" },
                    { 22, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Zeme" },
                    { 24, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Garāža" }
                });

            migrationBuilder.UpdateData(
                table: "VehicleAuctionDetails",
                keyColumn: "VehicleAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 18, 16, 59, 53, 916, DateTimeKind.Utc).AddTicks(3827));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "ItemAuctionDetails",
                keyColumn: "ItemAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 12, 14, 8, 58, 778, DateTimeKind.Utc).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 9,
                column: "Name",
                value: "Elektrotehnika");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 10,
                column: "Name",
                value: "Rūpniecības tehnika");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 11,
                column: "Name",
                value: "Instrumenti");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 12,
                column: "Name",
                value: "Cita manta");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 13,
                columns: new[] { "AuctionCategoryId", "Name" },
                values: new object[] { 3, "Dzīvoklis" });

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 14,
                columns: new[] { "AuctionCategoryId", "Name" },
                values: new object[] { 3, "Māja" });

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 15,
                columns: new[] { "AuctionCategoryId", "Name" },
                values: new object[] { 3, "Zeme" });

            migrationBuilder.UpdateData(
                table: "VehicleAuctionDetails",
                keyColumn: "VehicleAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 12, 14, 8, 58, 780, DateTimeKind.Utc).AddTicks(2011));
        }
    }
}
