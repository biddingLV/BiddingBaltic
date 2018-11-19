using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddCreatorCreatorIdToGetAuctionsStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[GetAuctions]
                            @StartDate date,
	                        @EndDate date
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
	                        WHERE auct.StartDate BETWEEN @StartDate AND @EndDate;
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
