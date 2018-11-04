using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddSearchAuctionListStoredProcedurev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetAuctions]
                            @StartDate date,
	                        @EndDate date
                        AS
                        BEGIN
                            select 
		                        auct.Id,
		                        auct.Description,
		                        auct.Brand,
		                        auct.Price,
		                        auct.Type,
		                        auct.StartDate,
		                        auct.EndDate
	                        from Auctions auct
	                        where auct.StartDate BETWEEN @StartDate AND @EndDate;
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
