using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemAuctionDetails");

            migrationBuilder.DropTable(
                name: "PropertyAuctionDetails");

            migrationBuilder.DropTable(
                name: "VehicleAuctionDetails");

            migrationBuilder.CreateTable(
                name: "AuctionDetails",
                columns: table => new
                {
                    AuctionDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionItemId = table.Column<int>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    IdentificationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    InspectionActive = table.Column<bool>(nullable: false),
                    Transmission = table.Column<string>(nullable: true),
                    FuelType = table.Column<string>(nullable: true),
                    EngineSize = table.Column<string>(nullable: true),
                    Axis = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    CadastreNumber = table.Column<string>(nullable: true),
                    MeasurementValue = table.Column<string>(nullable: true),
                    MeasurementType = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    FloorCount = table.Column<string>(nullable: true),
                    RoomCount = table.Column<string>(nullable: true),
                    Evaluation = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionDetails", x => x.AuctionDetailsId);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_AuctionItems_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalTable: "AuctionItems",
                        principalColumn: "AuctionItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_AuctionItemId",
                table: "AuctionDetails",
                column: "AuctionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_UserId",
                table: "AuctionDetails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionDetails");

            migrationBuilder.CreateTable(
                name: "ItemAuctionDetails",
                columns: table => new
                {
                    ItemAuctionDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionItemId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Evaluation = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAuctionDetails", x => x.ItemAuctionDetailsId);
                    table.ForeignKey(
                        name: "FK_ItemAuctionDetails_AuctionItems_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalTable: "AuctionItems",
                        principalColumn: "AuctionItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemAuctionDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAuctionDetails",
                columns: table => new
                {
                    PropertyAuctionDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionItemId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAuctionDetails", x => x.PropertyAuctionDetailsId);
                    table.ForeignKey(
                        name: "FK_PropertyAuctionDetails_AuctionItems_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalTable: "AuctionItems",
                        principalColumn: "AuctionItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyAuctionDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleAuctionDetails",
                columns: table => new
                {
                    VehicleAuctionDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionItemId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EngineSize = table.Column<string>(nullable: true),
                    Evaluation = table.Column<string>(nullable: true),
                    FuelType = table.Column<string>(nullable: true),
                    Gearbox = table.Column<string>(nullable: true),
                    IdentificationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    InspectionActive = table.Column<bool>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Power = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Transmission = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAuctionDetails", x => x.VehicleAuctionDetailsId);
                    table.ForeignKey(
                        name: "FK_VehicleAuctionDetails_AuctionItems_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalTable: "AuctionItems",
                        principalColumn: "AuctionItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleAuctionDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ItemAuctionDetails",
                columns: new[] { "ItemAuctionDetailsId", "AuctionItemId", "CreatedAt", "CreatedBy", "Deleted", "Evaluation", "LastUpdatedAt", "LastUpdatedBy", "ManufacturingDate", "Model", "UserId" },
                values: new object[] { 1, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, "In progress", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2019, 6, 18, 16, 59, 53, 915, DateTimeKind.Utc).AddTicks(4914), "In progress", null });

            migrationBuilder.InsertData(
                table: "PropertyAuctionDetails",
                columns: new[] { "PropertyAuctionDetailsId", "AuctionItemId", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "UserId" },
                values: new object[] { 1, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null });

            migrationBuilder.InsertData(
                table: "VehicleAuctionDetails",
                columns: new[] { "VehicleAuctionDetailsId", "AuctionItemId", "CreatedAt", "CreatedBy", "Deleted", "EngineSize", "Evaluation", "FuelType", "Gearbox", "IdentificationNumber", "InspectionActive", "LastUpdatedAt", "LastUpdatedBy", "Make", "ManufacturingDate", "Model", "Power", "RegistrationNumber", "Transmission", "UserId" },
                values: new object[] { 1, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "In progress", null, null, null, false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "In progress", new DateTime(2019, 6, 18, 16, 59, 53, 916, DateTimeKind.Utc).AddTicks(3827), "In progress", null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ItemAuctionDetails_AuctionItemId",
                table: "ItemAuctionDetails",
                column: "AuctionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAuctionDetails_UserId",
                table: "ItemAuctionDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAuctionDetails_AuctionItemId",
                table: "PropertyAuctionDetails",
                column: "AuctionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAuctionDetails_UserId",
                table: "PropertyAuctionDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAuctionDetails_AuctionItemId",
                table: "VehicleAuctionDetails",
                column: "AuctionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAuctionDetails_UserId",
                table: "VehicleAuctionDetails",
                column: "UserId");
        }
    }
}
