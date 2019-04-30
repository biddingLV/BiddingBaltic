using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddMissingCategoryTypesAndTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionVehicleDetails",
                columns: table => new
                {
                    AuctionVehicleDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Power = table.Column<string>(nullable: true),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    VehicleInspectionActive = table.Column<bool>(nullable: false),
                    EngineSize = table.Column<string>(nullable: true),
                    FuelType = table.Column<string>(nullable: true),
                    Transmission = table.Column<string>(nullable: true),
                    Gearbox = table.Column<string>(nullable: true),
                    VehicleRegistrationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    VehicleIdentificationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Evaluation = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionVehicleDetails", x => x.AuctionVehicleDetailsId);
                    table.ForeignKey(
                        name: "FK_AuctionVehicleDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 2,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 3,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 2,
                column: "Name",
                value: "Traktortehnika");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "Name",
                value: "Kravas auto");

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 4, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Mototehnika" },
                    { 5, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Ūdens transports" },
                    { 6, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Cits transports" },
                    { 7, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Biroja tehnika" },
                    { 8, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Elektrotehnika" },
                    { 9, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Rūpniecības tehnika" },
                    { 10, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Instrumenti" },
                    { 11, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Cita manta" },
                    { 12, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Dzīvoklis" },
                    { 13, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Māja" },
                    { 14, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Zeme" }
                });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "CategoryTypeId", "CategoryId", "TypeId" },
                values: new object[,]
                {
                    { 4, 1, 4 },
                    { 5, 1, 5 },
                    { 6, 1, 6 },
                    { 7, 2, 7 },
                    { 8, 2, 8 },
                    { 9, 2, 9 },
                    { 10, 2, 10 },
                    { 11, 2, 11 },
                    { 12, 3, 12 },
                    { 13, 3, 13 },
                    { 14, 3, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionVehicleDetails_UserId",
                table: "AuctionVehicleDetails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionVehicleDetails");

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 14);

            migrationBuilder.UpdateData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 2,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CategoryTypes",
                keyColumn: "CategoryTypeId",
                keyValue: 3,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 2,
                column: "Name",
                value: "Cita manta");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "Name",
                value: "Dzīvoklis");
        }
    }
}
