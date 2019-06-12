using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class PopulateUsersTableWithAdmins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ItemAuctionDetails",
                keyColumn: "ItemAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 12, 14, 8, 58, 778, DateTimeKind.Utc).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "MiddleName" },
                values: new object[] { "Test", "Admin", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "MiddleName" },
                values: new object[] { "Test", "User", "" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "CreatedBy", "Deleted", "Email", "FirstName", "LastName", "LastUpdatedAt", "LastUpdatedBy", "MiddleName", "RoleId", "UniqueIdentifier" },
                values: new object[,]
                {
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, "zanehaartman@gmail.com", "Zane", "", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "", 2, "" },
                    { 4, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, "kristaps.kerpe@gmail.com", "Kristaps", "", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "", 2, "" },
                    { 5, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, "j.jaunozols@gmail.com", "Jānis", "J", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "", 2, "" },
                    { 6, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, "janis.rihards.blazevics@gmail.com", "Jānis", "B", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "R", 2, "" }
                });

            migrationBuilder.UpdateData(
                table: "VehicleAuctionDetails",
                keyColumn: "VehicleAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 12, 14, 8, 58, 780, DateTimeKind.Utc).AddTicks(2011));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "ItemAuctionDetails",
                keyColumn: "ItemAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 10, 14, 5, 4, 699, DateTimeKind.Utc).AddTicks(5804));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "MiddleName" },
                values: new object[] { "Peteris", "Liepins", "Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "MiddleName" },
                values: new object[] { "Peteris", "Liepins", "User" });

            migrationBuilder.UpdateData(
                table: "VehicleAuctionDetails",
                keyColumn: "VehicleAuctionDetailsId",
                keyValue: 1,
                column: "ManufacturingDate",
                value: new DateTime(2019, 6, 10, 14, 5, 4, 701, DateTimeKind.Utc).AddTicks(977));
        }
    }
}
