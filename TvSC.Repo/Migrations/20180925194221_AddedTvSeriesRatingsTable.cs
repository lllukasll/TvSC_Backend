using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class AddedTvSeriesRatingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TvSeriesRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Story = table.Column<int>(nullable: false),
                    Music = table.Column<int>(nullable: false),
                    Effects = table.Column<int>(nullable: false),
                    Average = table.Column<int>(nullable: false),
                    TvShowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvSeriesRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvSeriesRatings_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TvSeriesRatings_TvShowId",
                table: "TvSeriesRatings",
                column: "TvShowId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TvSeriesRatings");
        }
    }
}
