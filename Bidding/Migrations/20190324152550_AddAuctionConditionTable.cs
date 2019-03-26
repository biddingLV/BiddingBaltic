using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bidding.Migrations
{
    public partial class AddAuctionConditionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.CreateTable(
                name: "AuctionConditions",
                columns: table => new
                {
                    AuctionConditionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionConditionName = table.Column<string>(maxLength: 50, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionConditions", x => x.AuctionConditionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DropTable(
                name: "AuctionConditions");
        }
    }
}
