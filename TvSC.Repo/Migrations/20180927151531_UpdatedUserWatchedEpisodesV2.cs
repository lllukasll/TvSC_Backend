using Microsoft.EntityFrameworkCore.Migrations;

namespace TvSC.Repo.Migrations
{
    public partial class UpdatedUserWatchedEpisodesV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchedTvSeries_TvShows_TvShowId",
                table: "UserWatchedTvSeries");

            migrationBuilder.RenameColumn(
                name: "TvShowId",
                table: "UserWatchedTvSeries",
                newName: "EpisodeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWatchedTvSeries_TvShowId",
                table: "UserWatchedTvSeries",
                newName: "IX_UserWatchedTvSeries_EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchedTvSeries_Episodes_EpisodeId",
                table: "UserWatchedTvSeries",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchedTvSeries_Episodes_EpisodeId",
                table: "UserWatchedTvSeries");

            migrationBuilder.RenameColumn(
                name: "EpisodeId",
                table: "UserWatchedTvSeries",
                newName: "TvShowId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWatchedTvSeries_EpisodeId",
                table: "UserWatchedTvSeries",
                newName: "IX_UserWatchedTvSeries_TvShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchedTvSeries_TvShows_TvShowId",
                table: "UserWatchedTvSeries",
                column: "TvShowId",
                principalTable: "TvShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
