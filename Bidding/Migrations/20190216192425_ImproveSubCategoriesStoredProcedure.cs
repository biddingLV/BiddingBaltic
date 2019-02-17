using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class ImproveSubCategoriesStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE GetSubCategoriesWithCount
                        AS
                        BEGIN
	                        SELECT
		                        ctyp.CategoryId,
		                        typ.TypeId,
		                        typ.TypeName,
		                        SUM(CASE
			                        WHEN ctyp.TypeId = typ.TypeId THEN 1
			                        ELSE 0
		                        END) AS TypeTotalCount
	                        FROM Types typ
	                        INNER JOIN CategoryTypes ctyp ON typ.TypeId = ctyp.TypeId
	                        INNER JOIN AuctionTypes atyp ON typ.TypeId = atyp.TypeId
	                        GROUP BY typ.TypeId,
				                     typ.TypeName,
				                     ctyp.CategoryId;
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
