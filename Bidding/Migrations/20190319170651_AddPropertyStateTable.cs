using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidding.Migrations
{
    public partial class AddPropertyStateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.CreateTable(
                name: "PropertyStates",
                columns: table => new
                {
                    PropertyStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PropertyStateName = table.Column<string>(maxLength: 50, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyStates", x => x.PropertyStateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
               migrationBuilder.DropTable(
                name: "PropertyStates");
        }
    }
}
