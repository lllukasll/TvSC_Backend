using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class AddedTvSeriesRatingModelV2 : Migration
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
                    TvShowId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_TvSeriesRatings_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TvSeriesRatings_TvShowId",
                table: "TvSeriesRatings",
                column: "TvShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TvSeriesRatings_UserId1",
                table: "TvSeriesRatings",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TvSeriesRatings");
        }
    }
}
