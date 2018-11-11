using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddCreatorToAuctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Auctions",
                defaultValue: "",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Auctions");
        }
    }
}
