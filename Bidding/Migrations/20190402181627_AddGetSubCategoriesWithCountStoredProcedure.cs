using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddGetSubCategoriesWithCountStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE PROCEDURE GetSubCategoriesWithCount
                AS
                BEGIN
	                SELECT
		                ctyp.CategoryId,
		                typ.TypeId,
		                typ.Name,
		                SUM(CASE
			                WHEN ctyp.TypeId = typ.TypeId THEN 1
			                ELSE 0
		                END) AS TypeTotalCount
	                FROM Types typ
	                INNER JOIN CategoryTypes ctyp ON typ.TypeId = ctyp.TypeId
	                INNER JOIN AuctionTypes atyp ON typ.TypeId = atyp.TypeId
	                GROUP BY typ.TypeId,
			                    typ.Name,
			                    ctyp.CategoryId;
                END;
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
