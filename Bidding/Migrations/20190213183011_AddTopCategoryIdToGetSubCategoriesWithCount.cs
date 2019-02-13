using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddTopCategoryIdToGetSubCategoriesWithCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[GetSubCategoriesWithCount]
                        AS
                        BEGIN
							SELECT
								ctyp.CategoryId,
								typ.TypeId,
								typ.TypeName,
								-- todo: kke: add info to the db!
								--SUM(CASE
								--	WHEN ctyp.TypeId = typ.TypeId THEN 1
								--	ELSE 0
								--END) AS SubCategoryTotalCount
								1 as TypeTotalCount
							FROM Types typ
							INNER JOIN CategoryTypes ctyp ON typ.TypeId = ctyp.TypeId
							GROUP BY typ.TypeId,
									 typ.TypeName,
									 ctyp.CategoryId
                        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE GetSubCategoriesWithCount;";

            migrationBuilder.Sql(sp);
        }
    }
}
