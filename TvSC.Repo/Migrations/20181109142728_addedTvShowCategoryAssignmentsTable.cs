using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class addedTvShowCategoryAssignmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TvShowCategoryAssignmentsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TvShowId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowCategoryAssignmentsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvShowCategoryAssignmentsEntity_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TvShowCategoryAssignmentsEntity_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TvShowCategoryAssignmentsEntity_CategoryId",
                table: "TvShowCategoryAssignmentsEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowCategoryAssignmentsEntity_TvShowId",
                table: "TvShowCategoryAssignmentsEntity",
                column: "TvShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TvShowCategoryAssignmentsEntity");
        }
    }
}
