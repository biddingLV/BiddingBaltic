using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddGetAuctionsStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE PROCEDURE GetAuctions
	                @selectedCategories CategoryIdArray READONLY,
	                @selectedTypes TypeIdArray READONLY
	                --@startDate date,
	                --@endDate date,
	                --@start int,
	                --@end int,
	                --@sortByColumn varchar(50),
	                --@sortingDirection varchar(50)
                AS
                BEGIN
	                DECLARE @categories TABLE ([CategoryId] [int] INDEX IX1 CLUSTERED);
	                DECLARE @types TABLE ([TypeId] [int] INDEX IX1 CLUSTERED);

	                INSERT INTO @categories (CategoryId)
	                SELECT CategoryId FROM @selectedCategories;

	                INSERT INTO @types (TypeId)
	                SELECT TypeId FROM @selectedTypes;

	                SELECT 
		                auct.AuctionId,
		                auct.Name as AuctionName,
		                auct.StartingPrice as AuctionStartingPrice,
		                auct.StartDate as AuctionStartDate,
		                auct.EndDate as AuctionEndDate,
		                asta.Name as AuctionStatusName
	                FROM Auctions auct
	                INNER JOIN AuctionCategories acat ON auct.AuctionId = acat.AuctionId
	                INNER JOIN AuctionTypes atyp ON auct.AuctionId = atyp.AuctionId
	                INNER JOIN AuctionStatuses asta ON auct.AuctionStatusId = asta.AuctionStatusId
	                WHERE acat.CategoryId IN (SELECT CategoryId FROM @categories)
	                AND atyp.TypeId IN (SELECT TypeId FROM @types)
	                --ORDER BY 
	                --	(CASE WHEN @sortByColumn = 'AuctionName' AND @sortingDirection  = 'asc' THEN auct.AuctionName END) ASC,    
	                --	(CASE WHEN @sortByColumn = 'AuctionName' AND @sortingDirection  = 'desc' THEN auct.AuctionName END) DESC,    
	                --	(CASE WHEN @sortByColumn = 'AuctionStartingPrice'  AND @sortingDirection  = 'asc' THEN auct.AuctionStartingPrice END) ASC,
	                --	(CASE WHEN @sortByColumn = 'AuctionStartingPrice'  AND @sortingDirection  = 'desc' THEN auct.AuctionStartingPrice END) DESC,
	                --	(CASE WHEN @sortByColumn = 'AuctionStartDate' AND @sortingDirection  = 'asc'  THEN auct.AuctionStartDate END) ASC,
	                --	(CASE WHEN @sortByColumn = 'AuctionStartDate' AND @sortingDirection  = 'desc' THEN auct.AuctionStartDate END) DESC,
	                --	(CASE WHEN @sortByColumn = 'AuctionEndDate' AND @sortingDirection  = 'asc'  THEN auct.AuctionEndDate END) ASC,
	                --	(CASE WHEN @sortByColumn = 'AuctionEndDate' AND @sortingDirection  = 'desc' THEN auct.AuctionEndDate END) DESC
	                --OFFSET @start ROWS
	                --FETCH NEXT @end ROWS ONLY
                END
            ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE GetAuctions;";

            migrationBuilder.Sql(sp);
        }
    }
}
