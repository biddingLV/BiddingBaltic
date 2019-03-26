using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class ImproveUsersAndAuctionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Auctions_AuctionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuctionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedBy",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Users",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Auctions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CreatedByUserUserId",
                table: "Auctions",
                column: "CreatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_CreatedByUserUserId",
                table: "Auctions",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_CreatedByUserUserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_CreatedByUserUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Auctions");

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedBy",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuctionId",
                table: "Users",
                column: "AuctionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Auctions_AuctionId",
                table: "Users",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
