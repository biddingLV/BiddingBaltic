using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddRequirementsToAuctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Auctions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "ConcurrencyStamp",
                value: "6fbc5339-8913-42a3-ae99-27903a789cfc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 200,
                column: "ConcurrencyStamp",
                value: "9e3d8d52-0785-4420-b23a-c82636934088");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "e7a68780-8aaf-4f18-8988-7367f7156715");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 400,
                column: "ConcurrencyStamp",
                value: "ed3aa8e6-b456-4bad-9962-2fec25ce4abf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "ConcurrencyStamp",
                value: "9e1162a1-2c52-4669-8371-c2969ea65278");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 200,
                column: "ConcurrencyStamp",
                value: "83656792-2d72-4a03-88ba-8fccc326271b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "0abc1188-d1ce-4f26-b48b-7039858152d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 400,
                column: "ConcurrencyStamp",
                value: "239217fa-a0bd-44e3-9a76-bead064ca23e");
        }
    }
}
