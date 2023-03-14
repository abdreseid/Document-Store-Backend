using Microsoft.EntityFrameworkCore.Migrations;

namespace DocAPI.Migrations
{
    public partial class document3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumetRelations",
                columns: table => new
                {
                    DocumentId = table.Column<int>(nullable: false),
                    UpdatedDocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumetRelations", x => new { x.DocumentId, x.UpdatedDocId });
                    table.ForeignKey(
                        name: "FK_DocumetRelations_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumetRelations_UpdateDocHistories_UpdatedDocId",
                        column: x => x.UpdatedDocId,
                        principalTable: "UpdateDocHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumetRelations_UpdatedDocId",
                table: "DocumetRelations",
                column: "UpdatedDocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumetRelations");
        }
    }
}
