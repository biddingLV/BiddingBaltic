using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddauctionValueAddedTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ValueAddedTax",
                table: "Auctions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "ConcurrencyStamp",
                value: "c0b6e0eb-35b1-4330-ae57-0a625ffd13c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 200,
                column: "ConcurrencyStamp",
                value: "f8d6b4ce-18db-4c0c-88f1-483f4d8d3a10");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "f4fcce40-a66e-46ad-94a4-dd2be44948ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 400,
                column: "ConcurrencyStamp",
                value: "23c7f016-c32a-483b-a469-4f02b281b033");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueAddedTax",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "ConcurrencyStamp",
                value: "c4f837c9-2934-41a8-88dc-73311aef6ca8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 200,
                column: "ConcurrencyStamp",
                value: "b83f098c-2ce5-4316-9594-84f818611cd0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "2acd26fd-b511-4559-a217-888447e6fe48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 400,
                column: "ConcurrencyStamp",
                value: "f0088a54-6b4e-4377-b374-00af7f358c75");
        }
    }
}
