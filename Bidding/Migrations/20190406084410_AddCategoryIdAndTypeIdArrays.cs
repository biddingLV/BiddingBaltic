using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddCategoryIdAndTypeIdArrays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE TYPE CategoryIdArray AS TABLE (CategoryId INT NULL);
                CREATE TYPE TypeIdArray AS TABLE(TypeId INT NULL);
            ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                DROP TYPE CategoryIdArray;
                DROP TYPE TypeIdArray;
            ";

            migrationBuilder.Sql(sp);
        }
    }
}
