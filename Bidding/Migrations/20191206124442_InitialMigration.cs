using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PermissionsInRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    IdentityId = table.Column<string>(maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionConditions",
                columns: table => new
                {
                    AuctionConditionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionConditions", x => x.AuctionConditionId);
                });

            migrationBuilder.CreateTable(
                name: "AuctionCreators",
                columns: table => new
                {
                    AuctionCreatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false),
                    ContactPhone = table.Column<string>(nullable: false),
                    ContactAddress = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionCreators", x => x.AuctionCreatorId);
                });

            migrationBuilder.CreateTable(
                name: "AuctionFormats",
                columns: table => new
                {
                    AuctionFormatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionFormats", x => x.AuctionFormatId);
                });

            migrationBuilder.CreateTable(
                name: "AuctionStatuses",
                columns: table => new
                {
                    AuctionStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionStatuses", x => x.AuctionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ItemConditions",
                columns: table => new
                {
                    ItemConditionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConditions", x => x.ItemConditionId);
                });

            migrationBuilder.CreateTable(
                name: "ModulesForUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(maxLength: 36, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowedPaidForModules = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulesForUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    Vehicles = table.Column<bool>(nullable: false),
                    Items = table.Column<bool>(nullable: false),
                    Estate = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMeasurementTypes",
                columns: table => new
                {
                    PropertyMeasurementTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMeasurementTypes", x => x.PropertyMeasurementTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "RolesToPermissions",
                columns: table => new
                {
                    RoleName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PermissionsInRole = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesToPermissions", x => x.RoleName);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFuelTypes",
                columns: table => new
                {
                    VehicleFuelTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFuelTypes", x => x.VehicleFuelTypeId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTransmissions",
                columns: table => new
                {
                    VehicleTransmissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTransmissions", x => x.VehicleTransmissionId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AuctionCategoryId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_Types_Categories_AuctionCategoryId",
                        column: x => x.AuctionCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    AuctionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    StartingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    ApplyTillDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AuctionCategoryId = table.Column<int>(nullable: false),
                    AuctionTypeId = table.Column<int>(nullable: false),
                    AuctionStatusId = table.Column<int>(nullable: false),
                    AuctionFormatId = table.Column<int>(nullable: false),
                    AuctionCreatorId = table.Column<int>(nullable: false),
                    AuctionImageContainer = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.AuctionId);
                    table.ForeignKey(
                        name: "FK_Auctions_Categories_AuctionCategoryId",
                        column: x => x.AuctionCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auctions_AuctionCreators_AuctionCreatorId",
                        column: x => x.AuctionCreatorId,
                        principalTable: "AuctionCreators",
                        principalColumn: "AuctionCreatorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auctions_AuctionFormats_AuctionFormatId",
                        column: x => x.AuctionFormatId,
                        principalTable: "AuctionFormats",
                        principalColumn: "AuctionFormatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auctions_AuctionStatuses_AuctionStatusId",
                        column: x => x.AuctionStatusId,
                        principalTable: "AuctionStatuses",
                        principalColumn: "AuctionStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auctions_Types_AuctionTypeId",
                        column: x => x.AuctionTypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuctionItems",
                columns: table => new
                {
                    AuctionItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AuctionId = table.Column<int>(nullable: false),
                    AuctionItemCategoryId = table.Column<int>(nullable: false),
                    AuctionItemTypeId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItems", x => x.AuctionItemId);
                    table.ForeignKey(
                        name: "FK_AuctionItems_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionItems_Categories_AuctionItemCategoryId",
                        column: x => x.AuctionItemCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionItems_Types_AuctionItemTypeId",
                        column: x => x.AuctionItemTypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuctionDetails",
                columns: table => new
                {
                    AuctionDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionItemId = table.Column<int>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    ManufacturingYear = table.Column<int>(nullable: true),
                    RegistrationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    IdentificationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    InspectionActive = table.Column<bool>(nullable: true),
                    TransmissionId = table.Column<int>(nullable: true),
                    FuelTypeId = table.Column<int>(nullable: true),
                    EngineSize = table.Column<string>(nullable: true),
                    Axis = table.Column<string>(nullable: true),
                    ConditionId = table.Column<int>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    CadastreNumber = table.Column<int>(nullable: true),
                    MeasurementValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MeasurementTypeId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    FloorCount = table.Column<int>(nullable: true),
                    RoomCount = table.Column<int>(nullable: true),
                    Evaluation = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
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
                        name: "FK_AuctionDetails_ItemConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "ItemConditions",
                        principalColumn: "ItemConditionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_VehicleFuelTypes_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "VehicleFuelTypes",
                        principalColumn: "VehicleFuelTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_PropertyMeasurementTypes_MeasurementTypeId",
                        column: x => x.MeasurementTypeId,
                        principalTable: "PropertyMeasurementTypes",
                        principalColumn: "PropertyMeasurementTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_VehicleTransmissions_TransmissionId",
                        column: x => x.TransmissionId,
                        principalTable: "VehicleTransmissions",
                        principalColumn: "VehicleTransmissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PermissionsInRole" },
                values: new object[,]
                {
                    { 100, "2c2bf5e2-911f-4428-bcbf-31dc37d0101d", null, "BasicUser", "BASICUSER", null },
                    { 300, "4092d17f-f346-49a2-b2f5-dea20a6a7208", null, "PageAdministrator", "PAGEADMINISTRATOR", null },
                    { 400, "cdb88a7e-b4ec-4f57-94e7-a996cbe277dc", null, "SuperAdministrator", "SUPERADMINISTRATOR", null },
                    { 200, "90d5b0f7-e0d8-4ef4-8177-7b96a8e36eeb", null, "AuctionCreator", "AUCTIONCREATOR", null }
                });

            migrationBuilder.InsertData(
                table: "AuctionConditions",
                columns: new[] { "AuctionConditionId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lietota" },
                    { 4, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Neapdzīvots" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Apdzīvots" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jauna" },
                    { 5, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Nepieciešams remonts" }
                });

            migrationBuilder.InsertData(
                table: "AuctionFormats",
                columns: new[] { "AuctionFormatId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cenu aptauja" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Izsole elektroniski" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Izsole klātienē" }
                });

            migrationBuilder.InsertData(
                table: "AuctionStatuses",
                columns: new[] { "AuctionStatusId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Aktīva" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Beigusies" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pārtraukta" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Transports" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Manta" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Nekustamais īpašums" }
                });

            migrationBuilder.InsertData(
                table: "ItemConditions",
                columns: new[] { "ItemConditionId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jauns" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lietots" }
                });

            migrationBuilder.InsertData(
                table: "PropertyMeasurementTypes",
                columns: new[] { "PropertyMeasurementTypeId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "a" },
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "m2" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ha" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 76, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Olaines novads" },
                    { 87, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rojas novads" },
                    { 86, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Riebiņu novads" },
                    { 85, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rēzeknes novads" },
                    { 84, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Raunas novads" },
                    { 83, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Priekuļu novads" },
                    { 82, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Priekules novads" },
                    { 81, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Preiļu novads" },
                    { 80, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pļaviņu novads" },
                    { 79, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pāvilostas novads" },
                    { 78, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pārgaujas novads" },
                    { 77, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ozolnieku novads" },
                    { 75, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ogres novads" },
                    { 64, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Limbažu novads" },
                    { 73, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Neretas novads" },
                    { 72, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Naukšēnu novads" },
                    { 71, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mērsraga novads" },
                    { 70, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mazsalacas novads" },
                    { 69, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mālpils novads" },
                    { 68, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Madonas novads" },
                    { 67, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ludzas novads" },
                    { 66, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lubānas novads" },
                    { 65, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Līvānu novads" },
                    { 88, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ropažu novads" },
                    { 63, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Līgatnes novads" },
                    { 62, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lielvārdes novads" },
                    { 74, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Nīcas novads" },
                    { 89, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rucavas novads" },
                    { 101, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Skrundas novads" },
                    { 91, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rūjienas novads" },
                    { 118, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Zilupes novads" },
                    { 117, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Viļānu novads" },
                    { 116, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Viļakas novads" },
                    { 115, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Viesītes novads" },
                    { 114, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ventspils novads" },
                    { 113, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Vecumnieku novads " },
                    { 112, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Vecpiebalgas novads" },
                    { 111, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Vārkavas novads" },
                    { 110, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Varakļānu novads" },
                    { 109, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Valkas novads" },
                    { 108, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Vaiņodes novads" },
                    { 107, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Tukuma novads" },
                    { 106, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Tērvetes novads" },
                    { 105, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Talsu novads" },
                    { 104, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Strenču novads" },
                    { 103, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Stopiņu novads" },
                    { 102, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Smiltenes novads" },
                    { 61, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kuldīgas novads" },
                    { 100, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Skrīveru novads" },
                    { 99, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Siguldas novads" },
                    { 98, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Sējas novads" },
                    { 97, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Saulkrastu novads" },
                    { 96, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Saldus novads" },
                    { 95, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Salaspils novads" },
                    { 94, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Salas novads" },
                    { 93, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Salacgrīvas novads" },
                    { 92, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rundāles novads" },
                    { 90, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rugāju novads" },
                    { 60, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Krustpils novads" },
                    { 53, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kārsavas novads" },
                    { 58, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Krāslavas novads" },
                    { 26, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Beverīnas novads" },
                    { 25, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bauskas novads" },
                    { 59, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Krimuldas novads" },
                    { 23, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Baltinavas novads" },
                    { 22, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Baldones novads" },
                    { 21, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Babītes novads" },
                    { 20, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Auces novads" },
                    { 19, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Apes novads" },
                    { 18, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Amatas novads" },
                    { 17, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Alūksnes novads" },
                    { 16, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Alsungas novads" },
                    { 15, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Alojas novads" },
                    { 27, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Brocēnu novads" },
                    { 14, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Aknīstes novads" },
                    { 12, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Aizkraukles novads" },
                    { 11, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Aglonas novads" },
                    { 10, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ādažu novads" },
                    { 9, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ventspils" },
                    { 8, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Valmiera" },
                    { 7, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rīga" },
                    { 6, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rēzekne" },
                    { 5, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Liepāja" },
                    { 4, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jūrmala" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jelgava" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jēkabpils" },
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Daugavpils" },
                    { 13, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Aizputes novads" },
                    { 28, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Burtnieku novads" },
                    { 24, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Balvu novads" },
                    { 30, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cēsu novads " },
                    { 57, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kokneses novads" },
                    { 56, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kocēnu novads" },
                    { 55, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ķekavas novads" },
                    { 54, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ķeguma novads" },
                    { 52, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kandavas novads" },
                    { 51, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jelgavas novads" },
                    { 50, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jēkabpils novads" },
                    { 29, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Carnikavas novads" },
                    { 48, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jaunpiebalgas novads" },
                    { 47, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jaunjelgavas novads" },
                    { 46, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Inčukalna novads" },
                    { 45, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ilūkstes novads" },
                    { 44, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ikšķiles novads" },
                    { 49, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jaunpils novads" },
                    { 42, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Gulbenes novads" },
                    { 31, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cesvaines novads" },
                    { 43, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Iecavas novads" },
                    { 33, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dagdas novads" },
                    { 34, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Daugavpils novads" },
                    { 35, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dobeles novads" },
                    { 32, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ciblas novads" },
                    { 37, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Durbes novads" },
                    { 38, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Engures novads" },
                    { 39, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ērgļu novads" },
                    { 40, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Garkalnes novads" },
                    { 41, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Grobiņas novads" },
                    { 36, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dundagas novads" }
                });

            migrationBuilder.InsertData(
                table: "RolesToPermissions",
                columns: new[] { "RoleName", "Description", "PermissionsInRole" },
                values: new object[,]
                {
                    { "AuctionCreator", "Can add, edit or delete own auctions", "AccessAdminPanel,CreateAuction,ChangeOwnAuction,RemoveOwnAuction" },
                    { "BasicUser", "Basic user", "BasicUser" },
                    { "SuperAdministrator", "Can do all possible actions", "AccessAll" },
                    { "PageAdministrator", "Can add, edit or delete auctions and users", "AccessAdminPanel,CreateAuction,ChangeAuction,RemoveAuction,ReadAllUsers" }
                });

            migrationBuilder.InsertData(
                table: "VehicleFuelTypes",
                columns: new[] { "VehicleFuelTypeId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 4, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Elektroniskais" },
                    { 5, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hibrīds" },
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Benzīns" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dīzelis" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Benzīns/Naftas gāze" }
                });

            migrationBuilder.InsertData(
                table: "VehicleTransmissions",
                columns: new[] { "VehicleTransmissionId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Automatiskā" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mehāniskā" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "AuctionCategoryId", "CreatedAt", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Vieglais transports līdz 3,5t" },
                    { 22, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Zeme" },
                    { 21, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Māja" },
                    { 20, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dzīvoklis" },
                    { 19, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cita manta" },
                    { 18, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Biroja mēbeles" },
                    { 17, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Sadzīves mēbeles" },
                    { 16, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Preču zīme" },
                    { 15, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Domeins" },
                    { 14, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Uzņēmums" },
                    { 13, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Veikala produkcija" },
                    { 12, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ražošanas materiāli" },
                    { 11, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Iekārtas" },
                    { 10, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Instrumenti" },
                    { 9, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Sadzīves tehnika" },
                    { 8, 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Biroja tehnika" },
                    { 7, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cits transports" },
                    { 6, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ūdens transports" },
                    { 5, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Piekabes" },
                    { 4, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mototehnika" },
                    { 3, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kravas auto" },
                    { 2, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Traktortehnika" },
                    { 23, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Telpa" },
                    { 24, 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Garāža" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_AuctionItemId",
                table: "AuctionDetails",
                column: "AuctionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_ConditionId",
                table: "AuctionDetails",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_FuelTypeId",
                table: "AuctionDetails",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_MeasurementTypeId",
                table: "AuctionDetails",
                column: "MeasurementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_RegionId",
                table: "AuctionDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_TransmissionId",
                table: "AuctionDetails",
                column: "TransmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItems_AuctionId",
                table: "AuctionItems",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItems_AuctionItemCategoryId",
                table: "AuctionItems",
                column: "AuctionItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItems_AuctionItemTypeId",
                table: "AuctionItems",
                column: "AuctionItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AuctionCategoryId",
                table: "Auctions",
                column: "AuctionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AuctionCreatorId",
                table: "Auctions",
                column: "AuctionCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AuctionFormatId",
                table: "Auctions",
                column: "AuctionFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AuctionStatusId",
                table: "Auctions",
                column: "AuctionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AuctionTypeId",
                table: "Auctions",
                column: "AuctionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Types_AuctionCategoryId",
                table: "Types",
                column: "AuctionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuctionConditions");

            migrationBuilder.DropTable(
                name: "AuctionDetails");

            migrationBuilder.DropTable(
                name: "ModulesForUsers");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropTable(
                name: "RolesToPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AuctionItems");

            migrationBuilder.DropTable(
                name: "ItemConditions");

            migrationBuilder.DropTable(
                name: "VehicleFuelTypes");

            migrationBuilder.DropTable(
                name: "PropertyMeasurementTypes");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "VehicleTransmissions");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "AuctionCreators");

            migrationBuilder.DropTable(
                name: "AuctionFormats");

            migrationBuilder.DropTable(
                name: "AuctionStatuses");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
