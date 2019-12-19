using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuctionExternalWebsite",
                table: "Auctions",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionExternalWebsite",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "ConcurrencyStamp",
                value: "6fa6bd71-a414-4477-8ffe-40f548a918b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 200,
                column: "ConcurrencyStamp",
                value: "da8da7ab-8cdd-4865-8d4c-114aac192f1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "f6a18d98-9d18-4c76-bf3a-b7efe34c6dcc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 400,
                column: "ConcurrencyStamp",
                value: "2bd22c74-ffcb-49c8-a427-08cbec370d26");
        }
    }
}
