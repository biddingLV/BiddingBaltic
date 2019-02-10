using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddGetAuctionsStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetAuctions]
	                        @startDate date,
	                        @endDate date,
	                        @start int,
	                        @end int,
	                        @sortByColumn varchar(50),
	                        @sortingDirection varchar(50)
                        AS
                        BEGIN
	                        SELECT 
		                        auct.AuctionId,
		                        auct.AuctionName,
		                        auct.AuctionStartingPrice,
		                        auct.AuctionStartDate,
		                        auct.AuctionEndDate
	                        FROM Auctions auct
	                        ORDER BY 
		                        (CASE WHEN @sortByColumn = 'AuctionName' AND @sortingDirection  = 'asc' THEN auct.AuctionName END) ASC,    
		                        (CASE WHEN @sortByColumn = 'AuctionName' AND @sortingDirection  = 'desc' THEN auct.AuctionName END) DESC,    
		                        (CASE WHEN @sortByColumn = 'AuctionStartingPrice'  AND @sortingDirection  = 'asc' THEN auct.AuctionStartingPrice END) ASC,
		                        (CASE WHEN @sortByColumn = 'AuctionStartingPrice'  AND @sortingDirection  = 'desc' THEN auct.AuctionStartingPrice END) DESC,
		                        (CASE WHEN @sortByColumn = 'AuctionStartDate' AND @sortingDirection  = 'asc'  THEN auct.AuctionStartDate END) ASC,
		                        (CASE WHEN @sortByColumn = 'AuctionStartDate' AND @sortingDirection  = 'desc' THEN auct.AuctionStartDate END) DESC,
		                        (CASE WHEN @sortByColumn = 'AuctionEndDate' AND @sortingDirection  = 'asc'  THEN auct.AuctionEndDate END) ASC,
		                        (CASE WHEN @sortByColumn = 'AuctionEndDate' AND @sortingDirection  = 'desc' THEN auct.AuctionEndDate END) DESC
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
