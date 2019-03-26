using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bidding.Migrations
{
    public partial class AddAuctionFormatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.CreateTable(
                name: "AuctionFormats",
                columns: table => new
                {
                    AuctionFormatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionFormatName = table.Column<string>(maxLength: 50, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionFormats", x => x.AuctionFormatId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DropTable(
                name: "AuctionFormats");
        }
    }
}
