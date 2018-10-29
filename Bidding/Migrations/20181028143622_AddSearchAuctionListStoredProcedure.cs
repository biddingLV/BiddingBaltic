using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddSearchAuctionListStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Newsletter",
                table: "Newsletter");

            migrationBuilder.RenameTable(
                name: "Newsletter",
                newName: "Newsletters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Newsletters",
                table: "Newsletters",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Newsletters",
                table: "Newsletters");

            migrationBuilder.RenameTable(
                name: "Newsletters",
                newName: "Newsletter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Newsletter",
                table: "Newsletter",
                column: "Id");
        }
    }
}
