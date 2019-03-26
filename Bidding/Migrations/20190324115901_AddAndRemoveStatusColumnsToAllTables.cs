using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddAndRemoveStatusColumnsToAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserStatus",
                table: "Users",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "TypeStatus",
                table: "Types",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "RoleStatus",
                table: "Roles",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "PermissionStatus",
                table: "Permissions",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "CategoryStatus",
                table: "Categories",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AuctionStatuses",
                newName: "Deleted");

            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Types",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Types",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Types",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "Types",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "Roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Permissions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Permissions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Permissions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "Permissions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AuctionStatuses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AuctionStatuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "AuctionStatuses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "AuctionStatuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Auctions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Auctions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Auctions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Auctions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "Auctions",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AuctionStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AuctionStatuses");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "AuctionStatuses");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "AuctionStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Users",
                newName: "UserStatus");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Types",
                newName: "TypeStatus");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Roles",
                newName: "RoleStatus");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Permissions",
                newName: "PermissionStatus");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Categories",
                newName: "CategoryStatus");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "AuctionStatuses",
                newName: "Status");
        }
    }
}
