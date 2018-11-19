using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddPaginationLogicToGetAuctionsv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[GetAuctions]
                            @startDate date,
	                        @endDate date,
							@start int,
							@end int,
							@sortByColumn varchar,
							@sortingDirection varchar
                        AS
                        BEGIN
							SELECT 
								auct.Id,
								auct.Description,
								auct.Name,
								auct.Price,
								auct.StartDate,
								auct.EndDate,
								auct.Creator,
								auct.CreatorId
							FROM Auctions auct
							--WHERE auct.StartDate BETWEEN @startDate AND @endDate;
							ORDER BY 
								CASE WHEN @sortByColumn = 'Name' AND @sortingDirection  = 'asc'	THEN auct.Name END ASC,    
								CASE WHEN @sortByColumn = 'Name' AND @sortingDirection  = 'desc' THEN auct.Name END DESC,    
								CASE WHEN @sortByColumn = 'Price'  AND @sortingDirection  = 'asc'	THEN auct.Price END ASC,
								CASE WHEN @sortByColumn = 'Price'  AND @sortingDirection  = 'desc' THEN auct.Price END DESC,
								CASE WHEN @sortByColumn = 'EndDate' AND @sortingDirection  = 'asc'	THEN auct.EndDate END ASC,
								CASE WHEN @sortByColumn = 'EndDate' AND @sortingDirection  = 'desc' THEN auct.EndDate END DESC,
								CASE WHEN @sortByColumn = 'Creator' AND @sortingDirection  = 'asc'	THEN auct.Creator END ASC,
								CASE WHEN @sortByColumn = 'Creator' AND @sortingDirection  = 'desc' THEN auct.Creator END DESC
							OFFSET @start ROWS
							FETCH NEXT @end ROWS ONLY
                        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE [dbo].[GetAuctions];";

            migrationBuilder.Sql(sp);
        }
    }
}
