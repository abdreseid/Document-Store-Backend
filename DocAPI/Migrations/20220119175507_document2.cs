using Microsoft.EntityFrameworkCore.Migrations;

namespace DocAPI.Migrations
{
    public partial class document2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UpdateDocHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "UpdateDocHistories");
        }
    }
}
