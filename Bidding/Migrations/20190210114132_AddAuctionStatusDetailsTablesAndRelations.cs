using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddAuctionStatusDetailsTablesAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionStatuses",
                columns: table => new
                {
                    AuctionStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionStatusName = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionStatuses", x => x.AuctionStatusId);
                });

            migrationBuilder.DropColumn(
                name: "AuctionType",
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
                name: "Evaluation",
                table: "AuctionDetails",
                newName: "AuctionStatusId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AuctionDetails",
                newName: "AuctionDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_AuctionId",
                table: "AuctionDetails",
                column: "AuctionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionDetails_Auctions_AuctionId",
                table: "AuctionDetails",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // todo: kke: this could be 100% wrong here, check db keys!
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionDetails_Auctions_AuctionId",
                table: "AuctionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionStatuses_AuctionDetails_AuctionForeignKey",
                table: "AuctionStatuses");

            migrationBuilder.DropIndex(
                name: "IX_AuctionDetails_AuctionId",
                table: "AuctionDetails");

            migrationBuilder.RenameColumn(
                name: "AuctionForeignKey",
                table: "AuctionStatuses",
                newName: "AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionStatuses_AuctionForeignKey",
                table: "AuctionStatuses",
                newName: "IX_AuctionStatuses_AuctionId");

            migrationBuilder.RenameColumn(
                name: "AuctionStatusId",
                table: "AuctionDetails",
                newName: "Evaluation");

            migrationBuilder.RenameColumn(
                name: "AuctionDetailsId",
                table: "AuctionDetails",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "AuctionStatusId",
                table: "Auctions",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuctionType",
                table: "AuctionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleIdentificationNumber",
                table: "AuctionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleRegistrationNumber",
                table: "AuctionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "AuctionDetails",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionStatuses_Auctions_AuctionId",
                table: "AuctionStatuses",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
