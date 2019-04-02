using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddCategoryIdArrayAndTypeIdArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE TYPE [CategoryIdArray] AS TABLE ([CategoryId] INT NULL);
                CREATE TYPE[TypeIdArray] AS TABLE([TypeId] INT NULL);
            ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE GetSubCategoriesWithCount;";

            migrationBuilder.Sql(sp);
        }
    }
}
