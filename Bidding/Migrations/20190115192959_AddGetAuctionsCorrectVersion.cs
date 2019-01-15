using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddGetAuctionsCorrectVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[GetAuctions]
                            @startDate date,
	                        @endDate date,
							@start int,
							@end int,
							@sortByColumn varchar(50),
							@sortingDirection varchar(50)
                        AS
                        BEGIN
							SELECT 
								auct.Id,
	                            auct.Description,
	                            auct.Name,
	                            auct.Price,
	                            auct.StartDate,
	                            auct.EndDate,
	                            acre.FirstName as CreatorFirstName,
	                            acre.LastName as CreatorLastName,
	                            auct.CreatorId
                            FROM Auctions auct,
	                             AuctionCreators acre
                            WHERE auct.CreatorId = acre.Id
							ORDER BY 
								(CASE WHEN @sortByColumn = 'name' AND @sortingDirection  = 'asc'	THEN auct.Name END) ASC,    
								(CASE WHEN @sortByColumn = 'name' AND @sortingDirection  = 'desc' THEN auct.Name END) DESC,    
								(CASE WHEN @sortByColumn = 'price'  AND @sortingDirection  = 'asc'	THEN auct.Price END) ASC,
								(CASE WHEN @sortByColumn = 'price'  AND @sortingDirection  = 'desc' THEN auct.Price END) DESC,
								(CASE WHEN @sortByColumn = 'endDate' AND @sortingDirection  = 'asc'	THEN auct.EndDate END) ASC,
								(CASE WHEN @sortByColumn = 'endDate' AND @sortingDirection  = 'desc' THEN auct.EndDate END) DESC,
								(CASE WHEN @sortByColumn = 'creator' AND @sortingDirection  = 'asc'	THEN acre.FirstName END) ASC,
								(CASE WHEN @sortByColumn = 'creator' AND @sortingDirection  = 'desc' THEN acre.FirstName END) DESC
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
