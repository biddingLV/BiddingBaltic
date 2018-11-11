using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddCreatorInfoToAuctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Auctions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Auctions");
        }
    }
}
