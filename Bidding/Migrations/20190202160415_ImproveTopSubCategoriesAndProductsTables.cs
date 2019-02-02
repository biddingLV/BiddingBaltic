using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class ImproveTopSubCategoriesAndProductsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryType_Categories_CategoryId",
                table: "CategoryType");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryType_Type_TypeId",
                table: "CategoryType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_Product_ProductId",
                table: "ProductDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeProduct_Product_ProductId",
                table: "TypeProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeProduct_Type_TypeId",
                table: "TypeProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeProduct",
                table: "TypeProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryType",
                table: "CategoryType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Type");

            migrationBuilder.RenameTable(
                name: "TypeProduct",
                newName: "TypeProducts");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "CategoryType",
                newName: "CategoryTypes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TypeProducts",
                newName: "TypeProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeProduct_TypeId",
                table: "TypeProducts",
                newName: "IX_TypeProducts_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeProduct_ProductId",
                table: "TypeProducts",
                newName: "IX_TypeProducts_ProductId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Types",
                newName: "TypeStatus");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Types",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CategoryTypes",
                newName: "CategoryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryType_TypeId",
                table: "CategoryTypes",
                newName: "IX_CategoryTypes_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryType_CategoryId",
                table: "CategoryTypes",
                newName: "IX_CategoryTypes_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "Types",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Products",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeProducts",
                table: "TypeProducts",
                column: "TypeProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTypes",
                table: "CategoryTypes",
                column: "CategoryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTypes_Categories_CategoryId",
                table: "CategoryTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTypes_Types_TypeId",
                table: "CategoryTypes",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_Products_ProductId",
                table: "ProductDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeProducts_Products_ProductId",
                table: "TypeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeProducts_Types_TypeId",
                table: "TypeProducts",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTypes_Categories_CategoryId",
                table: "CategoryTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTypes_Types_TypeId",
                table: "CategoryTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_Products_ProductId",
                table: "ProductDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeProducts_Products_ProductId",
                table: "TypeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeProducts_Types_TypeId",
                table: "TypeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeProducts",
                table: "TypeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTypes",
                table: "CategoryTypes");

            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.RenameTable(
                name: "TypeProducts",
                newName: "TypeProduct");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "CategoryTypes",
                newName: "CategoryType");

            migrationBuilder.RenameColumn(
                name: "TypeStatus",
                table: "Type",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Type",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TypeProductId",
                table: "TypeProduct",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TypeProducts_TypeId",
                table: "TypeProduct",
                newName: "IX_TypeProduct_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeProducts_ProductId",
                table: "TypeProduct",
                newName: "IX_TypeProduct_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Product",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryTypeId",
                table: "CategoryType",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTypes_TypeId",
                table: "CategoryType",
                newName: "IX_CategoryType_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTypes_CategoryId",
                table: "CategoryType",
                newName: "IX_CategoryType_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Type",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeProduct",
                table: "TypeProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryType",
                table: "CategoryType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryType_Categories_CategoryId",
                table: "CategoryType",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryType_Type_TypeId",
                table: "CategoryType",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_Product_ProductId",
                table: "ProductDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeProduct_Product_ProductId",
                table: "TypeProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeProduct_Type_TypeId",
                table: "TypeProduct",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
