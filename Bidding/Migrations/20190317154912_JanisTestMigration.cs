using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class JanisTestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ItemState",
                columns: table => new
                {
                    ItemStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemStateName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemState", x => x.ItemStateID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyState",
                columns: table => new
                {
                    PropertyStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PropertyStateName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyState", x => x.PropertyStateID);
                });

            migrationBuilder.CreateTable(
                name: "AuctionItemState",
                columns: table => new
                {
                    AuctionItemStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemStateId = table.Column<int>(nullable: false),
                    AuctionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItemState", x => x.AuctionItemStateId);
                    table.ForeignKey(
                        name: "FK_AuctionItemState_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionItemState_ItemStatus_ItemStateId",
                        column: x => x.ItemStateId,
                        principalTable: "ItemState",
                        principalColumn: "ItemStateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctionPropertyState",
                columns: table => new
                {
                    AuctionItemStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemStateId = table.Column<int>(nullable: false),
                    AuctionId = table.Column<int>(nullable: false),
                    PropertyStateID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionPropertyState", x => x.AuctionItemStateId);
                    table.ForeignKey(
                        name: "FK_AuctionPropertyState_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionPropertyState_PropertyState_PropertyStateID",
                        column: x => x.PropertyStateID,
                        principalTable: "PropertyState",
                        principalColumn: "PropertyStateID",
                        onDelete: ReferentialAction.Restrict);
                });

                migrationBuilder.AddColumn<int>(
                name: "AuctionItemStateName",
                table: "AuctionDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuctionPropertyStateName",
                table: "AuctionDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_AuctionItemStateName",
                table: "AuctionDetails",
                column: "AuctionItemStateName");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDetails_AuctionPropertyStateName",
                table: "AuctionDetails",
                column: "AuctionPropertyStateName");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItemState_AuctionId",
                table: "AuctionItemState",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItemState_ItemStateId",
                table: "AuctionItemState",
                column: "ItemStateId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionPropertyState_AuctionId",
                table: "AuctionPropertyState",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionPropertyState_PropertyStateID",
                table: "AuctionPropertyState",
                column: "PropertyStateID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionDetails_AuctionItemState_AuctionItemStateName",
                table: "AuctionDetails",
                column: "AuctionItemStateName",
                principalTable: "AuctionItemState",
                principalColumn: "AuctionItemStateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionDetails_AuctionPropertyState_AuctionPropertyStateName",
                table: "AuctionDetails",
                column: "AuctionPropertyStateName",
                principalTable: "AuctionPropertyState",
                principalColumn: "AuctionItemStateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionDetails_AuctionItemState_AuctionItemStateName",
                table: "AuctionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionDetails_AuctionPropertyState_AuctionPropertyStateName",
                table: "AuctionDetails");

            migrationBuilder.DropTable(
                name: "AuctionItemState");

            migrationBuilder.DropTable(
                name: "AuctionPropertyState");

            migrationBuilder.DropTable(
                name: "ItemState");

            migrationBuilder.DropTable(
                name: "PropertyState");

            migrationBuilder.DropIndex(
                name: "IX_AuctionDetails_AuctionItemStateName",
                table: "AuctionDetails");

            migrationBuilder.DropIndex(
                name: "IX_AuctionDetails_AuctionPropertyStateName",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "AuctionItemStateName",
                table: "AuctionDetails");

            migrationBuilder.DropColumn(
                name: "AuctionPropertyStateName",
                table: "AuctionDetails");
        }
    }
}
