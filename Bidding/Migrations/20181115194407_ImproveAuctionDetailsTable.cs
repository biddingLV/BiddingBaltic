using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class ImproveAuctionDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Model",
                table: "AuctionDetails",
                newName: "AuctionType");

            migrationBuilder.AddColumn<int>(
                name: "Evaluation",
                table: "AuctionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleIdentificationNumber",
                table: "AuctionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleRegistrationNumber",
                table: "AuctionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Year",
                table: "AuctionDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Evaluation",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "VehicleIdentificationNumber",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "VehicleRegistrationNumber",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AuctionDetails");

            migrationBuilder.RenameColumn(
                name: "AuctionType",
                table: "AuctionDetails",
                newName: "Model");
        }
    }
}
