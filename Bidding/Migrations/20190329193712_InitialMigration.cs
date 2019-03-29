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
                name: "Auctions",
                columns: table => new
                {
                    AuctionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionName = table.Column<string>(maxLength: 50, nullable: false),
                    AuctionStartingPrice = table.Column<int>(nullable: false),
                    AuctionStartDate = table.Column<DateTime>(nullable: false),
                    AuctionApplyDate = table.Column<DateTime>(nullable: false),
                    AuctionEndDate = table.Column<DateTime>(nullable: false),
                    AuctionStatusId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.AuctionId);
                });

            migrationBuilder.CreateTable(
                name: "AuctionStatuses",
                columns: table => new
                {
                    AuctionStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionStatusName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: true),
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
                    CategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    UserLastName = table.Column<string>(maxLength: 50, nullable: true),
                    UserEmail = table.Column<string>(maxLength: 50, nullable: false),
                    UserRoleId = table.Column<int>(nullable: false),
                    UserUniqueIdentifier = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedBy = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AuctionDetails",
                columns: table => new
                {
                    AuctionDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionStatusId = table.Column<int>(nullable: false),
                    AuctionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionDetails", x => x.AuctionDetailsId);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionDetails_AuctionStatuses_AuctionStatusId",
                        column: x => x.AuctionStatusId,
                        principalTable: "AuctionStatuses",
                        principalColumn: "AuctionStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctionCategories",
                columns: table => new
                {
                    AuctionCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    AuctionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionCategories", x => x.AuctionCategoryId);
                    table.ForeignKey(
                        name: "FK_AuctionCategories_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctionTypes",
                columns: table => new
                {
                    AuctionTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    AuctionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionTypes", x => x.AuctionTypeId);
                    table.ForeignKey(
                        name: "FK_AuctionTypes_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionTypes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    CategoryTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.CategoryTypeId);
                    table.ForeignKey(
                        name: "FK_CategoryTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTypes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeProduct",
                columns: table => new
                {
                    TypeProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeProduct", x => x.TypeProductId);
                    table.ForeignKey(
                        name: "FK_TypeProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeProduct_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuctionStatuses",
                columns: new[] { "AuctionStatusId", "AuctionStatusName", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy" },
                values: new object[,]
                {
                    { 1, "Aktīva", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null },
                    { 2, "Pārtraukta", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null },
                    { 3, "Beigusies", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "AuctionApplyDate", "AuctionEndDate", "AuctionName", "AuctionStartDate", "AuctionStartingPrice", "AuctionStatusId", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tesla Model 3", new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null },
                    { 2, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Penthouse tipa dzīvoklis Vecrīgas sirdī", new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null },
                    { 3, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vīna skapis", new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 900, 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy" },
                values: new object[,]
                {
                    { 1, "Transports", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null },
                    { 2, "Manta", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null },
                    { 3, "Nekustamais īpašums", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "User" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "TypeName" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Vieglais transports līdz 3,5t" },
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Cita manta" },
                    { 3, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "Dzīvoklis" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "CreatedBy", "Deleted", "LastUpdatedAt", "LastUpdatedBy", "UserEmail", "UserFirstName", "UserLastName", "UserRoleId", "UserUniqueIdentifier" },
                values: new object[,]
                {
                    { 2, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "dummyuser@bidding.lv", "Dummy", "User", 1, "" },
                    { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "dummyadmin@bidding.lv", "Dummy", "Admin", 2, "" }
                });

            migrationBuilder.InsertData(
                table: "AuctionCategories",
                columns: new[] { "AuctionCategoryId", "AuctionId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 3, 3, 2 },
                    { 2, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "CategoryTypeId", "CategoryId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionCategories_AuctionId",
                table: "AuctionCategories",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionCategories_CategoryId",
                table: "AuctionCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_AuctionId",
                table: "AuctionDetails",
                column: "AuctionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_AuctionStatusId",
                table: "AuctionDetails",
                column: "AuctionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionTypes_AuctionId",
                table: "AuctionTypes",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionTypes_TypeId",
                table: "AuctionTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTypes_CategoryId",
                table: "CategoryTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTypes_TypeId",
                table: "CategoryTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_FeatureId",
                table: "ProductDetail",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_ProductId",
                table: "ProductDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeProduct_ProductId",
                table: "TypeProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeProduct_TypeId",
                table: "TypeProduct",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionCategories");

            migrationBuilder.DropTable(
                name: "AuctionDetails");

            migrationBuilder.DropTable(
                name: "AuctionTypes");

            migrationBuilder.DropTable(
                name: "CategoryTypes");

            migrationBuilder.DropTable(
                name: "ProductDetail");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TypeProduct");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AuctionStatuses");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
