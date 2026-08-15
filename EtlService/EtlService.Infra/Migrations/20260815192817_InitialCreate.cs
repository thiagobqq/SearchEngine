using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtlService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndexedPages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexedPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchPostings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    TermFrequency = table.Column<int>(type: "int", nullable: false),
                    InTitle = table.Column<bool>(type: "bit", nullable: false),
                    Positions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchPostings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DocumentFrequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchTerms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndexedPages_Url",
                table: "IndexedPages",
                column: "Url");

            migrationBuilder.CreateIndex(
                name: "IX_SearchPostings_PageId",
                table: "SearchPostings",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchPostings_TermId",
                table: "SearchPostings",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchPostings_TermId_PageId",
                table: "SearchPostings",
                columns: new[] { "TermId", "PageId" });

            migrationBuilder.CreateIndex(
                name: "IX_SearchTerms_Term",
                table: "SearchTerms",
                column: "Term",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndexedPages");

            migrationBuilder.DropTable(
                name: "SearchPostings");

            migrationBuilder.DropTable(
                name: "SearchTerms");
        }
    }
}
