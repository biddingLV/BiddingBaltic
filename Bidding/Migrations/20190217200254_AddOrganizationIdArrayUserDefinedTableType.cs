using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddOrganizationIdArrayUserDefinedTableType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE TYPE [dbo].[OrganizationIdArray] AS TABLE ([OrganizationId] INT NULL);";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP TYPE [dbo].[OrganizationIdArray]";

            migrationBuilder.Sql(sp);
        }
    }
}
